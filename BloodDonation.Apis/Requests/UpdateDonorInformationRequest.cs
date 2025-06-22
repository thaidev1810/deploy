using BloodDonation.Domain.Users;

namespace BloodDonation.Apis.Requests;

public class UpdateDonorInformationRequest
{
    public Guid DonorInfoId { get; set; }
    public decimal Weight { get; set; }
    public decimal Height { get; set; }
    public MedicalStatus MedicalStatus { get; set; }
}