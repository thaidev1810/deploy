using BloodDonation.Domain.Donations;
using BloodDonation.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BloodDonation.Infrastructure.Configurations;

public class DonationHistoryConfiguration : IEntityTypeConfiguration<DonationHistory>
{
    public void Configure(EntityTypeBuilder<DonationHistory> builder)
    {
        builder.HasKey(x => x.DonationId);

        builder.Property(x => x.Date).IsRequired();

        builder.Property(x => x.Status)
            .HasConversion<string>()
            .IsRequired()
            .HasDefaultValue(DonationHistoryStatus.Completed);

        builder.HasOne(x => x.Donor)
            .WithMany(u => u.DonationHistories)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Request)
            .WithMany()
            .HasForeignKey(x => x.RequestId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.ConfirmedByUser)
            .WithMany(u => u.ConfirmedDonations)
            .HasForeignKey(x => x.ConfirmedBy)
            .OnDelete(DeleteBehavior.Restrict);
    }
}