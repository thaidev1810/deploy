using BloodDonation.Application.Abstraction.Authentication;
using BloodDonation.Application.Abstraction.Data;
using BloodDonation.Application.Abstraction.Messaging;
using BloodDonation.Application.BloodDonation.CreateDonationMatch;
using BloodDonation.Application.Users.UpdateUser;
using BloodDonation.Domain.Bloods;
using BloodDonation.Domain.Bloods.Errors;
using BloodDonation.Domain.Common;
using BloodDonation.Domain.Donations;
using BloodDonation.Domain.Users.Errors;
using Microsoft.EntityFrameworkCore;

namespace BloodDonation.Application.BloodDonation.CreateDonationRequest;

public class CreateDonationRequestCommandHandler(IDbContext context, IUserContext userContext) : ICommandHandler<CreateDonationRequestCommand,CreateDonationRequestResponse>
{
    public async Task<Result<CreateDonationRequestResponse>> Handle(CreateDonationRequestCommand request,
        CancellationToken cancellationToken)
    {
        var userId = userContext.UserId;
        var user = await context.Users.FirstOrDefaultAsync(u => u.UserId == request.UserId, cancellationToken);
        if (user == null)
        {
            return Result.Failure<CreateDonationRequestResponse>(UserErrors.NotFound(request.UserId));
        }

        var donationRequest = new DonationRequest
        {
            RequestId = Guid.NewGuid(),
            UserId = userId,
            BloodTypeId = request.BloodTypeId,
            AmountBlood = request.AmountBlood,
            ComponentType = request.ComponentType,
            RequestTime = DateTime.UtcNow,
            Deadline = request.Deadline,
            IsEmergency = request.IsEmergency,
            UrgencyLevel = request.UrgencyLevel,
            EmergencyContactName = request.EmergencyContactName,
            EmergencyContactPhone = request.EmergencyContactPhone,
            Note = request.Note,
            Status = DonationRequestStatus.Pending
        };

        context.DonationRequests.Add(donationRequest);
        await context.SaveChangesAsync(cancellationToken);

        if (user.IsDonor == true)
        {
            var bloodStored = await context.BloodStored
                .FirstOrDefaultAsync(b => b.BloodTypeId == request.BloodTypeId, cancellationToken);
            
            if (bloodStored == null)
            {
                return Result.Failure<CreateDonationRequestResponse>(BloodErrors.BloodTypeNotFound);
            }
            bloodStored.Quantity += request.AmountBlood;
            bloodStored.LastUpdated = DateTime.UtcNow;
            

            context.DonationsHistory.Add(new DonationHistory
            {
                DonationId = Guid.NewGuid(),
                UserId = userId,
                RequestId = donationRequest.RequestId,
                Date = DateTime.UtcNow,
                Status = DonationHistoryStatus.Completed,
                ConfirmedBy = userId
            });

            donationRequest.Status = DonationRequestStatus.Fulfilled;
        }
        else
        {
            var stored = await context.BloodStored
                .FirstOrDefaultAsync(x => x.BloodTypeId == request.BloodTypeId, cancellationToken);

            var available = stored?.Quantity ?? 0;

            if (available >= request.AmountBlood)
            {
                stored!.Quantity -= request.AmountBlood;
                stored.LastUpdated = DateTime.UtcNow;

                context.DonationsHistory.Add(new DonationHistory
                {
                    DonationId = Guid.NewGuid(),
                    UserId = userId,
                    RequestId = donationRequest.RequestId,
                    Date = DateTime.UtcNow,
                    Status = DonationHistoryStatus.Completed,
                    ConfirmedBy = userId
                });

                donationRequest.Status = DonationRequestStatus.Fulfilled;
            }
            else
            {
                var matcher = new AutoMatchDonorsForRequestHandler(context);
                await matcher.MatchDonorsAsync(donationRequest, cancellationToken);
            }
        }

        await context.SaveChangesAsync(cancellationToken);
        return Result.Success(new CreateDonationRequestResponse
        {
            RequestId = donationRequest.RequestId,
            Message = "Donation request created successfully."
        });
    }
}