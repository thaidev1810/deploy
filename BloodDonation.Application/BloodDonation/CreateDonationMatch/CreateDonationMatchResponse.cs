namespace BloodDonation.Application.BloodDonation.CreateDonationMatch;

public class CreateDonationMatchResponse
{
    public Guid MatchId { get; set; }
    public string Message { get; set; } = string.Empty;
}