using BloodDonation.Domain.Users;

namespace BloodDonation.Application.Users.UpdateUser;

public class UpdateUserResponse
{
    public Guid Id { get; init; }
    public string FullName { get; init; }
    public string Email { get; init; }
    public UserRole Role { get; init; }
    public UserStatus Status { get; init; }
    public string? BloodType { get; init; }
    public DateOnly DateOfBirth { get; init; }
    public UserGender Gender { get; init; }
    public string Address { get; init; }
    public string Phone { get; init; }
    public bool? IsDonor { get; init; }

    public UpdateUserResponse(User user)
    {
        Id = user.UserId;
        FullName = user.Name;
        Email = user.Email;
        Role = user.Role;
        Status = user.Status;
        BloodType = user.BloodType;
        DateOfBirth = user.DateOfBirth;
        Gender = user.Gender;
        Address = user.Address;
        Phone = user.Phone;
        IsDonor = user.IsDonor;
    }
}