using BloodDonation.Application.Abstraction.Authentication;
using BloodDonation.Application.Abstraction.Data;
using BloodDonation.Application.Abstraction.Messaging;
using BloodDonation.Domain.Common;
using BloodDonation.Domain.Donations;

namespace BloodDonation.Application.BloodDonation.CreateDonationMatch;

public class CreateDonationMatchCommandHandler(IDbContext context) : ICommandHandler<CreateDonationMatchCommand,CreateDonationMatchResponse>
{
    public async Task<Result<CreateDonationMatchResponse>> Handle(CreateDonationMatchCommand request,
        CancellationToken cancellationToken)
    {
        var match = new DonationMatch
        {
            MatchId = Guid.NewGuid(),
            RequestId = request.RequestId,
            DonorId = request.DonorId,
            MatchedTime = DateTime.UtcNow,
            Status = DonationMatchStatus.Pending
        };

        context.DonationMatches.Add(match);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success(new CreateDonationMatchResponse
        {
            MatchId = match.MatchId,
            Message = "Donor matched successfully."
        });
    }
    
}