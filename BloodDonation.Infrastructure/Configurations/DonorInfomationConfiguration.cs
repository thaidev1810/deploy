using BloodDonation.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BloodDonation.Infrastructure.Configurations;

public class DonorInformationConfiguration : IEntityTypeConfiguration<DonorInformation>
{
    public void Configure(EntityTypeBuilder<DonorInformation> builder)
    {
        builder.HasKey(x => x.DonorInfoId);

        builder.Property(x => x.Weight).IsRequired();
        builder.Property(x => x.Height).IsRequired();

        builder.Property(x => x.MedicalStatus)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(x => x.LastChecked).IsRequired();

        builder.HasOne(d => d.User)
            .WithOne(u => u.DonorInformation)
            .HasForeignKey<DonorInformation>(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}