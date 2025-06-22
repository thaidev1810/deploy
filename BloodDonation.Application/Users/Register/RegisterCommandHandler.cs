using BloodDonation.Application.Abstraction.Authentication;
using BloodDonation.Application.Abstraction.Data;
using BloodDonation.Application.Abstraction.Messaging;
using BloodDonation.Domain.Common;
using BloodDonation.Domain.Users;
using BloodDonation.Domain.Users.Errors;
using Microsoft.EntityFrameworkCore;

namespace BloodDonation.Application.Users.Register;

public class RegisterCommandHandler(
    IDbContext context,
    IPasswordHasher passwordHasher
) : ICommandHandler<RegisterCommand, RegisterResponse>
{
    public async Task<Result<RegisterResponse>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        if (await context.Users.AnyAsync(u => u.Email.Trim().ToLower() == command.Email.Trim().ToLower(), cancellationToken))
        {
            return Result.Failure<RegisterResponse>(UserErrors.EmailNotUnique);
        }

        var hashedPassword = passwordHasher.Hash(command.Password);
        var user = new User
        {
            UserId = Guid.NewGuid(),
            Name = command.Name,
            Email = command.Email,
            Password = hashedPassword,
            DateOfBirth = command.DateOfBirth,
            Gender = command.Gender,
            Address = command.Address,
            Phone = command.Phone,
            Role = UserRole.Member,
            Status = UserStatus.Active,
            IsDonor = true,
            IsVerified = false
        };

        context.Users.Add(user);
        await context.SaveChangesAsync(cancellationToken);

        return new RegisterResponse
        {
            Name = user.Name,
            Email = user.Email,
            DateOfBirth = user.DateOfBirth,
            Gender = user.Gender,
            Address = user.Address,
            Phone = user.Phone
        };
    }
}