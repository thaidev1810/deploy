using BloodDonation.Application.Abstraction.Messaging;

namespace BloodDonation.Application.Bloods.UpdateBloodStored;

public class UpdateBloodStoredCommand : ICommand<UpdateBloodStoredResponse>
{
    public Guid StoredId { get; set; }
    public string BloodTypeName { get; set; } = default!;
    public int? Quantity { get; set; }
}