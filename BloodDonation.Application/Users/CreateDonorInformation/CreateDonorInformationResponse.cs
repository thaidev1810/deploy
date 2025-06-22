using BloodDonation.Domain.Users;

namespace BloodDonation.Application.Users.CreateDonorInformation;

public class CreateDonorInformationResponse
{
    public Guid DonorInfoId { get; set; }
    public Guid UserId { get; set; }
    public decimal Weight { get; set; }
    public decimal Height { get; set; }
    public MedicalStatus MedicalStatus { get; set; }
    public DateTime LastChecked { get; set; }
    
}