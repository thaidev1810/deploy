using BloodDonation.Application.Abstraction.Messaging;
using BloodDonation.Domain.Bloods;
using BloodDonation.Domain.Donations;

namespace BloodDonation.Application.BloodDonation.CreateDonationRequest;

public class CreateDonationRequestCommand : ICommand<CreateDonationRequestResponse>
{
    public Guid UserId { get; set; }
    public Guid BloodTypeId { get; set; }
    public int AmountBlood { get; set; }
    public BloodComponentType ComponentType { get; set; }
    public DateTime? Deadline { get; set; }
    public bool IsEmergency { get; set; }
    public UrgencyLevel UrgencyLevel { get; set; }
    public string? EmergencyContactName { get; set; }
    public string? EmergencyContactPhone { get; set; }
    public string? Note { get; set; }
}