namespace BloodDonation.Application.Users.CreateHealthForm;

public class CreateHealthFormResponse
{
    public Guid FormId { get; set; }
    public Guid UserId { get; set; }
    public DateTime CreatedAt { get; set; }
}