using BloodDonation.Application.Abstraction.Authentication;
using BloodDonation.Application.Abstraction.Data;
using BloodDonation.Application.Abstraction.Messaging;
using BloodDonation.Domain.Common;
using BloodDonation.Domain.Users;
using BloodDonation.Domain.Users.Errors;
using Microsoft.EntityFrameworkCore;

namespace BloodDonation.Application.Users.CreateUser;

public class CreateUserCommandHandler (IDbContext context, 
    IPasswordHasher passwordHasher
): ICommandHandler<CreateUserCommand, CreateUserCommandResponse>
{
    public async Task<Result<CreateUserCommandResponse>> Handle(CreateUserCommand command,
        CancellationToken cancellationToken)
    {
        if (await context.Users.AnyAsync(u => u.Email.Trim().ToLower() == command.Email.Trim().ToLower(), cancellationToken))
        {
            return Result.Failure<CreateUserCommandResponse>(UserErrors.EmailNotUnique);
        }

        if (command.Role == UserRole.Admin)
        {
            bool adminExists = await context.Users
                .AnyAsync(u => u.Role == UserRole.Admin, cancellationToken);

            if (adminExists)
            {
                return Result.Failure<CreateUserCommandResponse>(UserErrors.AdminAlreadyExists);
            }
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
            Role = command.Role,
            Status = UserStatus.Active,
            IsDonor = false
        };

        context.Users.Add(user);
        await context.SaveChangesAsync(cancellationToken);

        return new CreateUserCommandResponse
        {
            Id = user.UserId,
            Name = user.Name,
            Email = user.Email,
            DateOfBirth = user.DateOfBirth,
            Gender = user.Gender,
            Address = user.Address,
            Phone = user.Phone,
            Role = user.Role
        };

    }

}