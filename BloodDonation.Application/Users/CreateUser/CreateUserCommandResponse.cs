using BloodDonation.Domain.Users;

namespace BloodDonation.Application.Users.CreateUser;

public sealed record CreateUserCommandResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Email { get; init; }
    public DateOnly DateOfBirth { get; init; }
    public UserGender Gender { get; init; }
    public string Address { get; init; }
    public string Phone { get; init; }
    public UserRole Role { get; init; }
}