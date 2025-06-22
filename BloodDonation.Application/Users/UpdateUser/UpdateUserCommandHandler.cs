using BloodDonation.Application.Abstraction.Authentication;
using BloodDonation.Application.Abstraction.Data;
using BloodDonation.Application.Abstraction.Messaging;
using BloodDonation.Application.Users.GetCurrentUser;
using BloodDonation.Domain.Common;
using BloodDonation.Domain.Users;
using BloodDonation.Domain.Users.Errors;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BloodDonation.Application.Users.UpdateUser;

public sealed class UpdateUserCommandHandler(IDbContext context, IUserContext userContext, ISender sender)
    : ICommandHandler<UpdateUserCommand, UpdateUserResponse>
{
    public async Task<Result<UpdateUserResponse>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.UserId == request.UserId, cancellationToken);
        if (user == null)
        {
            return Result.Failure<UpdateUserResponse>(UserErrors.NotFound(request.UserId));
        }
        
        // var currentUserResult = await sender.Send(new GetCurrentUserQuery(), cancellationToken);
        // var currentUser = currentUserResult.Value;
        // bool isStaff = currentUser.Role == UserRole.Staff.ToString();
        

        user.Name = request.FullName ?? user.Name;
        user.Email = request.Email ?? user.Email;
        user.DateOfBirth = request.DateOfBirth ?? user.DateOfBirth;
        user.Gender = request.Gender ?? user.Gender;
        user.Address = request.Address ?? user.Address;
        user.Phone = request.Phone ?? user.Phone;

        // if (isStaff)
        // {
            user.Role = request.Role ?? user.Role;
            user.Status = request.Status ?? user.Status;
            user.IsDonor = request.IsDonor ?? user.IsDonor;
            user.BloodType = request.BloodType ?? user.BloodType;
        // }

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success(new UpdateUserResponse(user));
    }
}
