using BloodDonation.Application.Abstraction.Authentication;
using BloodDonation.Application.Abstraction.Data;
using BloodDonation.Application.Abstraction.Messaging;
using BloodDonation.Domain.Bloods;
using BloodDonation.Domain.Bloods.Errors;
using BloodDonation.Domain.Common;
using BloodDonation.Domain.Donations;
using BloodDonation.Domain.Donations.Errors;
using Microsoft.EntityFrameworkCore;

namespace BloodDonation.Application.BloodDonation.ConfirmDonationMatch;

public class ConfirmDonationMatchCommandHandler(IDbContext context, IUserContext userContext) : ICommandHandler<ConfirmDonationMatchCommand>
{
    public async Task<Result> Handle(ConfirmDonationMatchCommand request, CancellationToken cancellationToken)
    {
        var match = await context.DonationMatches
            .Include(m => m.Request)
            .FirstOrDefaultAsync(m => m.MatchId == request.MatchId, cancellationToken);

        if (match == null)
            return Result.Failure(DonationMatchErrors.MatchNotFound);

        if (match.DonorId != userContext.UserId)
            return Result.Failure(DonationMatchErrors.NotDonor);

        match.Status = request.IsAccepted ? DonationMatchStatus.Confirmed : DonationMatchStatus.Cancelled;
        match.ConfirmedTime = DateTime.UtcNow;

        if (request.IsAccepted)
        {
            var bloodStored = await context.BloodStored
                .FirstOrDefaultAsync(b => b.BloodTypeId == match.Request.BloodTypeId, cancellationToken);
            
            if (bloodStored == null)
            {
                return Result.Failure(BloodErrors.BloodTypeNotFound);
            }
            
            bloodStored.Quantity += match.Request.AmountBlood;
            bloodStored.LastUpdated = DateTime.UtcNow;
                
            context.DonationsHistory.Add(new DonationHistory
            {
                DonationId = Guid.NewGuid(),
                UserId = match.DonorId,
                RequestId = match.RequestId,
                Date = DateTime.UtcNow,
                Status = DonationHistoryStatus.Completed,
                ConfirmedBy = userContext.UserId
            });

            match.Request.Status = DonationRequestStatus.Fulfilled;
        }

        await context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}