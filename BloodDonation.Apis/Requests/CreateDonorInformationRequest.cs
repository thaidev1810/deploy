using BloodDonation.Domain.Users;

namespace BloodDonation.Apis.Requests;

public class CreateDonorInformationRequest
{
    public decimal Weight { get; set; }
    public decimal Height { get; set; }
    public MedicalStatus MedicalStatus { get; set; }
}