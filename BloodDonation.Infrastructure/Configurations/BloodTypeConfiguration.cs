using BloodDonation.Domain.Bloods;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BloodDonation.Infrastructure.Configurations;

public class BloodTypeConfiguration : IEntityTypeConfiguration<BloodType>
{
    public void Configure(EntityTypeBuilder<BloodType> builder)
    {
        builder.HasKey(x => x.BloodTypeId);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(10);

        builder.HasIndex(x => x.Name)
            .IsUnique();

        builder.Property(x => x.Description)
            .HasMaxLength(500);
        
        builder.HasMany(x => x.CompatibleFrom)
            .WithOne(x => x.FromBloodType)
            .HasForeignKey(x => x.FromBloodTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.CompatibleTo)
            .WithOne(x => x.ToBloodType)
            .HasForeignKey(x => x.ToBloodTypeId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasData(
            new BloodType
            {
                BloodTypeId = Guid.Parse("2b0f96e4-9052-4d68-a937-9adfc9d231d1"),
                Name = "A+",
                Description = "A positive blood type"
            },
            new BloodType
            {
                BloodTypeId = Guid.Parse("0f5f77fb-2bd4-4aeb-9bd4-bb56745c8845"),
                Name = "A-",
                Description = "A negative blood type"
            },
            new BloodType
            {
                BloodTypeId = Guid.Parse("91baf3d9-759f-4bb8-82a4-3d9d645d91b7"),
                Name = "B+",
                Description = "B positive blood type"
            },
            new BloodType
            {
                BloodTypeId = Guid.Parse("82f33bfb-7fa4-432e-8735-1c0e5c2f99f7"),
                Name = "B-",
                Description = "B negative blood type"
            },
            new BloodType
            {
                BloodTypeId = Guid.Parse("edc95a1c-0c3f-4a61-a104-f949109e7c0f"),
                Name = "AB+",
                Description = "AB positive blood type (universal plasma donor)"
            },
            new BloodType
            {
                BloodTypeId = Guid.Parse("1479d6c3-0c85-4cb7-a2c4-894c35e21eb1"),
                Name = "AB-",
                Description = "AB negative blood type"
            },
            new BloodType
            {
                BloodTypeId = Guid.Parse("b160fa12-dfa5-44c7-a179-6ef0f3c7c28c"),
                Name = "O+",
                Description = "O positive blood type (most common)"
            },
            new BloodType
            {
                BloodTypeId = Guid.Parse("62ef305e-755a-4651-9ed7-6fc4b4061e79"),
                Name = "O-",
                Description = "O negative blood type (universal donor)"
            }
        );

    }
}