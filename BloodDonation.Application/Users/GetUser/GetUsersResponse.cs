using BloodDonation.Domain.Users;

namespace BloodDonation.Application.Users.GetUser;

public sealed class GetUsersResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public UserRole Role { get; set; }
    public UserGender Gender { get; set; }
    public UserStatus Status { get; set; }
    public string? BloodType { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public bool? IsDonor { get; set; }
}