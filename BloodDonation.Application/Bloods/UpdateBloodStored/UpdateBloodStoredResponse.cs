using BloodDonation.Domain.Bloods;

namespace BloodDonation.Application.Bloods.UpdateBloodStored;

public class UpdateBloodStoredResponse
{
    public Guid StoredId { get; set; }
    public string BloodTypeName { get; set; }
    public int Quantity { get; set; }
    public DateTime LastUpdated { get; set; }

    public UpdateBloodStoredResponse(BloodStored stored)
    {
        StoredId = stored.StoredId;
        BloodTypeName = stored.BloodType.Name;
        Quantity = stored.Quantity;
        LastUpdated = stored.LastUpdated;
    }
}