using BloodDonation.Domain.Bloods;
using BloodDonation.Domain.Donations;
using BloodDonation.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BloodDonation.Infrastructure.Configurations;

public class DonationRequestConfiguration : IEntityTypeConfiguration<DonationRequest>
{
    public void Configure(EntityTypeBuilder<DonationRequest> builder)
    {
        builder.HasKey(x => x.RequestId);

        builder.Property(x => x.AmountBlood).IsRequired();
        builder.Property(x => x.RequestTime).IsRequired();
        builder.Property(x => x.Deadline).IsRequired();
        builder.Property(x => x.IsEmergency).HasDefaultValue(false);

        builder.Property(x => x.UrgencyLevel)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(x => x.Status)
            .HasConversion<string>()
            .IsRequired()
            .HasDefaultValue(DonationRequestStatus.Pending);

        builder.Property(x => x.EmergencyContactName).HasMaxLength(100);
        builder.Property(x => x.EmergencyContactPhone).HasMaxLength(20);
        builder.Property(x => x.Note).HasMaxLength(500);

        builder.HasOne(x => x.User)
            .WithMany(u => u.DonationRequests)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.BloodType)
            .WithMany()
            .HasForeignKey(x => x.BloodTypeId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.Property(x => x.ComponentType)
            .HasConversion<string>() 
            .IsRequired();

    }
}