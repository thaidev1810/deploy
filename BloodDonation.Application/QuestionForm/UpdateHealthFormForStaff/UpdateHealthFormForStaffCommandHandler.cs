using BloodDonation.Application.Abstraction.Authentication;
using BloodDonation.Application.Abstraction.Data;
using BloodDonation.Application.Abstraction.Messaging;
using BloodDonation.Domain.Common;
using BloodDonation.Domain.QuestionForm;
using BloodDonation.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace BloodDonation.Application.QuestionForm.UpdateHealthFormForStaff;

public class UpdateHealthFormForStaffCommandHandler(IDbContext context, IUserContext userContext) : ICommandHandler<UpdateHealthFormForStaffCommand, UpdateHealthFormForStaffResponse>
{
   

    public async Task<Result<UpdateHealthFormForStaffResponse>> Handle(UpdateHealthFormForStaffCommand command, CancellationToken cancellationToken)
    {
        var form = await context.HealthForms
            .FirstOrDefaultAsync(f => f.FormId == command.FormId, cancellationToken);

        if (form == null)
        {
            return Result.Failure<UpdateHealthFormForStaffResponse>(
                Error.NotFound("HealthForm.NotFound", $"Health form with ID '{command.FormId}' not found."));
        }

        form.Status = command.Status;
        form.ApprovedBy = userContext.UserId;
        form.ApprovedAt = DateTime.UtcNow;

            // Lấy thông tin user (staff) theo UserId để lấy tên
        var staffUser = await context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.UserId == userContext.UserId, cancellationToken);

        form.ApprovedByStaffName = staffUser?.Name;
        

        await context.SaveChangesAsync(cancellationToken);

        return new UpdateHealthFormForStaffResponse
        {
            FormId = form.FormId,
            UserId = form.UserId,
            Status = form.Status.ToString(),
            ApprovedBy = form.ApprovedBy,
            ApprovedAt = form.ApprovedAt,
            ApprovedByStaffName = form.ApprovedByStaffName
        };
    }
}
