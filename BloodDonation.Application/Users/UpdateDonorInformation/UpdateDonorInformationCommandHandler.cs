using BloodDonation.Application.Abstraction.Authentication;
using BloodDonation.Application.Abstraction.Data;
using BloodDonation.Application.Abstraction.Messaging;
using BloodDonation.Domain.Common;
using BloodDonation.Domain.Users;
using BloodDonation.Domain.Users.Errors;
using Microsoft.EntityFrameworkCore;

namespace BloodDonation.Application.Users.UpdateDonorInformation;

public class UpdateDonorInformationCommandHandler(IDbContext context, IUserContext userContext) : ICommandHandler<UpdateDonorInformationCommand, UpdateDonorInformationResponse>
{
    public async Task<Result<UpdateDonorInformationResponse>> Handle(UpdateDonorInformationCommand request,
        CancellationToken cancellationToken)
    {
        var currentUserId = userContext.UserId;

        var currentUser = await context.Users
            .FirstOrDefaultAsync(u => u.UserId == currentUserId, cancellationToken);

        if (currentUser == null)
            return Result.Failure<UpdateDonorInformationResponse>(UserErrors.NotFound(userContext.UserId));

        var donorInfo = await context.DonorInformation
            .Include(d => d.User)
            .FirstOrDefaultAsync(d => d.DonorInfoId == request.DonorInfoId, cancellationToken);

        if (donorInfo == null)
            return Result.Failure<UpdateDonorInformationResponse>(UserErrors.NotFound(donorInfo.DonorInfoId));

        var targetUser = donorInfo.User;

        if (targetUser == null || targetUser.IsDonor != true)
            return Result.Failure<UpdateDonorInformationResponse>(UserErrors.IsNotDonor);

        // Nếu current user là member thì chỉ được update chính mình
        if (currentUser.Role == UserRole.Member && targetUser.UserId != currentUser.UserId)
            return Result.Failure<UpdateDonorInformationResponse>(UserErrors.MemberCannotUpdateOthers);

        donorInfo.Weight = request.Weight;
        donorInfo.Height = request.Height;
        donorInfo.MedicalStatus = request.MedicalStatus;
        donorInfo.LastChecked = DateTime.UtcNow;

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success(new UpdateDonorInformationResponse
        {
            DonorInfoId = donorInfo.DonorInfoId,
            UserId = donorInfo.UserId,
            Weight = donorInfo.Weight,
            Height = donorInfo.Height,
            MedicalStatus = donorInfo.MedicalStatus,
            LastChecked = donorInfo.LastChecked,
        });
    }
}