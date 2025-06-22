using BloodDonation.Application.Abstraction.Authentication;
using BloodDonation.Application.Abstraction.Data;
using BloodDonation.Application.Abstraction.Messaging;
using BloodDonation.Domain.Common;
using BloodDonation.Domain.QuestionForm.Errors;
using Microsoft.EntityFrameworkCore;

namespace BloodDonation.Application.Users.DeleteHealthForm
{
    public class DeleteHealthFormCommandHandler(IDbContext context, IUserContext userContext) : ICommandHandler<DeleteHealthFormCommand, DeleteHealthFormResponse>
    {
        
        public async Task<Result<DeleteHealthFormResponse>> Handle(DeleteHealthFormCommand command, CancellationToken cancellationToken)
        {
            var currentUserId = userContext.UserId;

            var healthForm = await context.HealthForms
                .FirstOrDefaultAsync(hf => hf.UserId == currentUserId, cancellationToken);

            if (healthForm == null)
            {
                return Result.Failure<DeleteHealthFormResponse>(QuestionFormErrors.HealthFormNotFound);
            }

            context.HealthForms.Remove(healthForm);
            await context.SaveChangesAsync(cancellationToken);

            return Result.Success(new DeleteHealthFormResponse
            {
                FormId = healthForm.FormId
            });
        }
    }
}