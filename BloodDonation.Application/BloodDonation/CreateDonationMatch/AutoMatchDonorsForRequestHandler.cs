using BloodDonation.Application.Abstraction.Data;
using BloodDonation.Domain.Donations;
using Microsoft.EntityFrameworkCore;

namespace BloodDonation.Application.BloodDonation.CreateDonationMatch;

public class AutoMatchDonorsForRequestHandler(IDbContext context)
{
    public async Task<List<DonationMatch>> MatchDonorsAsync(DonationRequest request, CancellationToken cancellationToken)
    {
        var compatibleDonors = await context.Users
            .Where(u => u.IsDonor == true && u.BloodType != null)
            .Join(context.BloodTypes, 
                user => user.BloodType,
                type => type.Name,
                (user, bloodType) => new { user, bloodType })
            .Join(context.BloodCompatibility,
                donor => new { From = donor.bloodType.BloodTypeId, request.ComponentType },
                comp => new { From = comp.FromBloodTypeId, comp.ComponentType },
                (donor, comp) => new { donor, comp })
            .Where(x => x.comp.ToBloodTypeId == request.BloodTypeId)
            .Select(x => x.donor)
            .ToListAsync(cancellationToken);

        var matches = compatibleDonors.Select(donor => new DonationMatch
        {
            MatchId = Guid.NewGuid(),
            RequestId = request.RequestId,
            DonorId = donor.user.UserId,
            MatchedTime = DateTime.UtcNow,
            Status = DonationMatchStatus.Pending
        }).ToList();

        context.DonationMatches.AddRange(matches);
        await context.SaveChangesAsync(cancellationToken);

        return matches;
    }
}