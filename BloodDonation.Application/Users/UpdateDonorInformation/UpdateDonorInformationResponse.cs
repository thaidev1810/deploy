using BloodDonation.Domain.Users;

namespace BloodDonation.Application.Users.UpdateDonorInformation;

public class UpdateDonorInformationResponse
{
    public Guid DonorInfoId { get; set; }
    public Guid UserId { get; set; }
    public decimal Weight { get; set; }
    public decimal Height { get; set; }
    public MedicalStatus MedicalStatus { get; set; }
    public DateTime LastChecked { get; set; }
    
    // public GetUsers? User { get; set; }
}

// public class GetUsers
// {
//     public string Name { get; set; }
//     public string Email { get; set; }
//     public string? BloodType { get; set; }
// }