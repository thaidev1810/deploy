using BloodDonation.Domain.Users;

namespace BloodDonation.Application.Users.GetDonorInformation;

public class GetDonorInformationResponse
{
    public Guid DonorInfoId { get; set; }
    public decimal Weight { get; set; }
    public decimal Height { get; set; }
    public MedicalStatus MedicalStatus { get; set; }
    public DateTime LastChecked { get; set; }
    
    public GetUsers? User { get; set; }
}

public class GetUsers
{
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string? BloodType { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public UserGender Gender { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public UserRole Role { get; set; }
    public UserStatus Status { get; set; }
}