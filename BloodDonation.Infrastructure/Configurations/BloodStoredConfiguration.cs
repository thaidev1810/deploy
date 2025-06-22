using BloodDonation.Domain.Bloods;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BloodDonation.Infrastructure.Configurations;

public class BloodStoredConfiguration : IEntityTypeConfiguration<BloodStored>
{
    public void Configure(EntityTypeBuilder<BloodStored> builder)
    {
        builder.HasKey(x => x.StoredId);

        builder.Property(x => x.Quantity)
            .IsRequired();

        builder.Property(x => x.LastUpdated)
            .IsRequired();
        
        builder.HasOne(x => x.BloodType) 
            .WithMany()
            .HasForeignKey(x => x.BloodTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(
            new BloodStored
            {
                StoredId = Guid.Parse("10000000-0000-0000-0000-000000000001"),
                BloodTypeId = Guid.Parse("2b0f96e4-9052-4d68-a937-9adfc9d231d1"), // A+
                Quantity = 0,
                LastUpdated = DateTime.UtcNow
            },
            new BloodStored
            {
                StoredId = Guid.Parse("10000000-0000-0000-0000-000000000002"),
                BloodTypeId = Guid.Parse("0f5f77fb-2bd4-4aeb-9bd4-bb56745c8845"), // A−
                Quantity = 0,
                LastUpdated = DateTime.UtcNow
            },
            new BloodStored
            {
                StoredId = Guid.Parse("10000000-0000-0000-0000-000000000003"),
                BloodTypeId = Guid.Parse("91baf3d9-759f-4bb8-82a4-3d9d645d91b7"), // B+
                Quantity = 0,
                LastUpdated = DateTime.UtcNow
            },
            new BloodStored
            {
                StoredId = Guid.Parse("10000000-0000-0000-0000-000000000004"),
                BloodTypeId = Guid.Parse("82f33bfb-7fa4-432e-8735-1c0e5c2f99f7"), // B−
                Quantity = 0,
                LastUpdated = DateTime.UtcNow
            },
            new BloodStored
            {
                StoredId = Guid.Parse("10000000-0000-0000-0000-000000000005"),
                BloodTypeId = Guid.Parse("edc95a1c-0c3f-4a61-a104-f949109e7c0f"), // AB+
                Quantity = 0,
                LastUpdated = DateTime.UtcNow
            },
            new BloodStored
            {
                StoredId = Guid.Parse("10000000-0000-0000-0000-000000000006"),
                BloodTypeId = Guid.Parse("1479d6c3-0c85-4cb7-a2c4-894c35e21eb1"), // AB−
                Quantity = 0,
                LastUpdated = DateTime.UtcNow
            },
            new BloodStored
            {
                StoredId = Guid.Parse("10000000-0000-0000-0000-000000000007"),
                BloodTypeId = Guid.Parse("b160fa12-dfa5-44c7-a179-6ef0f3c7c28c"), // O+
                Quantity = 0,
                LastUpdated = DateTime.UtcNow
            },
            new BloodStored
            {
                StoredId = Guid.Parse("10000000-0000-0000-0000-000000000008"),
                BloodTypeId = Guid.Parse("62ef305e-755a-4651-9ed7-6fc4b4061e79"), // O−
                Quantity = 0,
                LastUpdated = DateTime.UtcNow
            }
        );

    }
}