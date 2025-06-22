using BloodDonation.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BloodDonation.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.UserId);

        builder.HasIndex(x => x.Email).IsUnique();

        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Email).IsRequired();
        builder.Property(x => x.Password).IsRequired();
        builder.Property(x => x.BloodType).HasMaxLength(10);

        builder.Property(x => x.Gender)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(x => x.Address).HasMaxLength(500);
        builder.Property(x => x.Phone).HasMaxLength(20);

        builder.Property(x => x.Role)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(x => x.Status)
            .HasConversion<string>()
            .IsRequired()
            .HasDefaultValue(UserStatus.Active);

        builder.Property(x => x.IsDonor).HasDefaultValue(false);
        builder.Property(x => x.LastDonationDate).IsRequired(false);
        builder.Property(x => x.IsVerified).IsRequired().HasDefaultValue(false);

        builder.HasMany(x => x.DonationRequests)
            .WithOne(r => r.User)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.DonationMatches)
            .WithOne(m => m.Donor)
            .HasForeignKey(m => m.DonorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.DonationHistories)
            .WithOne(h => h.Donor)
            .HasForeignKey(h => h.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.ConfirmedDonations)
            .WithOne(h => h.ConfirmedByUser)
            .HasForeignKey(h => h.ConfirmedBy)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.BlogPosts)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.DonorInformation)
            .WithOne(u => u.User)
            .HasForeignKey<DonorInformation>(d => d.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
    }
}