using BloodDonation.Domain.Bloods;
using BloodDonation.Domain.Users;

namespace BloodDonation.Domain.Donations;

public class DonationRequest
{
    public Guid RequestId { get; set; }
    public Guid BloodTypeId { get; set; }
    public Guid UserId { get; set; }
    public int AmountBlood { get; set; }
    public BloodComponentType ComponentType { get; set; }
    public DateTime RequestTime { get; set; }
    public DateTime? Deadline { get; set; }
    public bool IsEmergency { get; set; }
    public UrgencyLevel UrgencyLevel { get; set; }
    public string? EmergencyContactName { get; set; }
    public string? EmergencyContactPhone { get; set; }
    public DonationRequestStatus Status { get; set; }
    public string? Note { get; set; }
    
    public User? User { get; set; }
    public BloodType? BloodType { get; set; }
}