using BloodDonation.Domain.Users;

namespace BloodDonation.Apis.Requests;

public class RegisterRequest
{
    public string Name { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }
    public DateOnly DateOfBirth { get; init; }
    public UserGender Gender { get; init; }
    public string Address { get; init; }
    public string Phone { get; init; }
}