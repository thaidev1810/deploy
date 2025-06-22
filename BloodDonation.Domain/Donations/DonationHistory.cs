using BloodDonation.Domain.Users;

namespace BloodDonation.Domain.Donations;

public class DonationHistory
{
    public Guid DonationId { get; set; }
    public Guid UserId { get; set; }
    public Guid RequestId { get; set; }
    public DateTime Date { get; set; }
    public DonationHistoryStatus Status { get; set; }
    public Guid ConfirmedBy { get; set; }
    
    public User? Donor { get; set; }
    public DonationRequest? Request { get; set; }
    public User? ConfirmedByUser { get; set; }
}