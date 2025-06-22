using BloodDonation.Domain.Users;

namespace BloodDonation.Domain.Donations;

public class DonationMatch
{
    public Guid MatchId { get; set; }
    public Guid RequestId { get; set; }
    public Guid DonorId { get; set; }
    public DateTime MatchedTime { get; set; }
    public DateTime? ConfirmedTime { get; set; }
    public DonationMatchStatus Status { get; set; }
    
    public DonationRequest? Request { get; set; }
    public User? Donor { get; set; }
}