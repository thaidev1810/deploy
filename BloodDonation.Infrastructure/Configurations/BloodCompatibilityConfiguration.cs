using BloodDonation.Domain.Bloods;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BloodDonation.Infrastructure.Configurations;

public class BloodCompatibilityConfiguration : IEntityTypeConfiguration<BloodCompatibility>
{
    public void Configure(EntityTypeBuilder<BloodCompatibility> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.FromBloodType)
            .WithMany()
            .HasForeignKey(x => x.FromBloodTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.ToBloodType)
            .WithMany()
            .HasForeignKey(x => x.ToBloodTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.ComponentType)
            .HasConversion<string>()
            .IsRequired();
    }
}