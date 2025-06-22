namespace BloodDonation.Application.BloodDonation.CreateDonationRequest;

public class CreateDonationRequestResponse
{
    public Guid RequestId { get; set; }
    public string Message { get; set; } = string.Empty;
}