using BloodDonation.Application.Abstraction.Data;
using BloodDonation.Application.Abstraction.Messaging;
using BloodDonation.Application.Abstraction.Authentication;
using BloodDonation.Domain.Common;
using BloodDonation.Domain.QuestionForm;
using BloodDonation.Domain.QuestionForm.Errors;
using Microsoft.EntityFrameworkCore;

namespace BloodDonation.Application.Users.CreateHealthForm;

public class CreateHealthFormCommandHandler(
    IDbContext context,
    IUserContext userContext
) : ICommandHandler<CreateHealthFormCommand, CreateHealthFormResponse>
{
    public async Task<Result<CreateHealthFormResponse>> Handle(CreateHealthFormCommand command, CancellationToken cancellationToken)
    {
        var userId = userContext.UserId;

        // Check if user already has a HealthForm
        var existingForm = await context.HealthForms
            .AnyAsync(f => f.UserId == userId, cancellationToken);
        if (existingForm)
        {
            return Result.Failure<CreateHealthFormResponse>(QuestionFormErrors.HealthFormExist);
        }

        // Validate UserId exists
        var userExists = await context.Users.AnyAsync(u => u.UserId == userId, cancellationToken);
        if (!userExists)
        {
            return Result.Failure<CreateHealthFormResponse>(QuestionFormErrors.UserNotFound(userId));
        }

        // Validate all QuestionIds exist
        var questionIds = command.Answers.Select(a => a.QuestionId).ToList();
        var existingQuestionIds = await context.HealthQuestions
            .Where(q => questionIds.Contains(q.QuestionId))
            .Select(q => q.QuestionId)
            .ToListAsync(cancellationToken);

        var missingQuestions = questionIds.Except(existingQuestionIds).ToList();
        if (missingQuestions.Any())
        {
            return Result.Failure<CreateHealthFormResponse>(QuestionFormErrors.QuestionNotFound(missingQuestions));
        }

        // Create new HealthForm
        var healthForm = new HealthForm
        {
            FormId = Guid.NewGuid(),
            UserId = userId,
            CreatedAt = DateTime.UtcNow,
            Status = FormStatus.Pending,
            Answers = command.Answers.Select(a => new HealthAnswer
            {
                AnswerId = Guid.NewGuid(),
                QuestionId = a.QuestionId,
                Answer = a.Answer
            }).ToList()
        };

        context.HealthForms.Add(healthForm);
        await context.SaveChangesAsync(cancellationToken);

        var response = new CreateHealthFormResponse
        {
            FormId = healthForm.FormId,
            UserId = healthForm.UserId,
            CreatedAt = healthForm.CreatedAt
        };

        return response;
    }
}
