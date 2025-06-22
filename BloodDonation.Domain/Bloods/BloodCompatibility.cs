namespace BloodDonation.Domain.Bloods;

public class BloodCompatibility
{
    public Guid Id { get; set; }

    // From blood type
    public Guid FromBloodTypeId { get; set; }
    public BloodType FromBloodType { get; set; } = default!;

    // To blood type
    public Guid ToBloodTypeId { get; set; }
    public BloodType ToBloodType { get; set; } = default!;

    public BloodComponentType ComponentType { get; set; }
}