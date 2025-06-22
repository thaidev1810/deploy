using BloodDonation.Domain.Bloods;

namespace BloodDonation.Application.Bloods.GetBloodStored;

public class GetBloodStoredResponse
{
    public Guid StoredId { get; set; }
    public Guid BloodTypeId { get; set; }
    public string BloodTypeName { get; set; } = default!;
    public int Quantity { get; set; }
    public DateTime LastUpdated { get; set; }
}