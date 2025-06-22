using BloodDonation.Application.Abstraction.Authentication;
using BloodDonation.Application.Abstraction.Data;
using BloodDonation.Application.Abstraction.Messaging;
using BloodDonation.Application.Users.CreateUser;
using BloodDonation.Domain.Common;
using BloodDonation.Domain.Users;
using BloodDonation.Domain.Users.Errors;
using Microsoft.EntityFrameworkCore;

namespace BloodDonation.Application.Users.CreateDonorInformation;

public class CreateDonorInformationCommandHandler(IDbContext context, IUserContext userContext) : ICommandHandler<CreateDonorInformationCommand, CreateDonorInformationResponse>
{
    public async Task<Result<CreateDonorInformationResponse>> Handle(CreateDonorInformationCommand command,
        CancellationToken cancellationToken)
    {
       var userId = userContext.UserId;
       var user = await context.Users.FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);
       
       if (user is null)
           return Result.Failure<CreateDonorInformationResponse>(UserErrors.NotFound(userId));

       if (!(user.IsDonor ?? false))
           return Result.Failure<CreateDonorInformationResponse>(UserErrors.IsNotDonor);

       if (user.Role is UserRole.Admin or UserRole.Staff)
           return Result.Failure<CreateDonorInformationResponse>(UserErrors.IsNotMember);
       
       var exists = await context.DonorInformation
           .AnyAsync(x => x.UserId == user.UserId, cancellationToken);

       if (exists)
           return Result.Failure<CreateDonorInformationResponse>(UserErrors.DonorInforAlreadyExist);
       
       var donorInfo = new DonorInformation
       {
           UserId = user.UserId,
           Weight = command.Weight,
           Height = command.Height,
           MedicalStatus = command.MedicalStatus,
           LastChecked = DateTime.UtcNow
       };

       await context.DonorInformation.AddAsync(donorInfo, cancellationToken);
       await context.SaveChangesAsync(cancellationToken);

       var response = new CreateDonorInformationResponse
       {
        DonorInfoId = donorInfo.DonorInfoId,
        UserId = donorInfo.UserId,
        Weight = donorInfo.Weight,
        Height = donorInfo.Height,
        MedicalStatus = donorInfo.MedicalStatus,
        LastChecked = donorInfo.LastChecked
       };
       return response;
    }
}