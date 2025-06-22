using BloodDonation.Domain.Users;

namespace BloodDonation.Apis.Requests;

public class CreateUserRequest
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public UserGender Gender { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public UserRole Role { get; set; }
}