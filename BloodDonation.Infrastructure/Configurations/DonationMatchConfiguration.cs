using BloodDonation.Domain.Donations;
using BloodDonation.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BloodDonation.Infrastructure.Configurations;

public class DonationMatchConfiguration : IEntityTypeConfiguration<DonationMatch>
{
    public void Configure(EntityTypeBuilder<DonationMatch> builder)
    {
        builder.HasKey(x => x.MatchId);

        builder.Property(x => x.MatchedTime).IsRequired();

        builder.Property(x => x.Status)
            .HasConversion<string>()
            .IsRequired()
            .HasDefaultValue(DonationMatchStatus.Pending);

        builder.HasOne(x => x.Request)
            .WithMany()
            .HasForeignKey(x => x.RequestId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Donor)
            .WithMany(u => u.DonationMatches)
            .HasForeignKey(x => x.DonorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}