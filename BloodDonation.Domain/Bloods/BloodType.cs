namespace BloodDonation.Domain.Bloods;

public class BloodType
{
    public Guid BloodTypeId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    
    public ICollection<BloodCompatibility> CompatibleFrom { get; set; } = new List<BloodCompatibility>();
    public ICollection<BloodCompatibility> CompatibleTo { get; set; } = new List<BloodCompatibility>();
}