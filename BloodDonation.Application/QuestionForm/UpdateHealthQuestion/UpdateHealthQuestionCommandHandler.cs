using BloodDonation.Application.Abstraction.Data;
using BloodDonation.Application.Abstraction.Messaging;
using BloodDonation.Domain.QuestionForm;
using BloodDonation.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace BloodDonation.Application.QuestionForm.UpdateHealthQuestion;

public class UpdateHealthQuestionCommandHandler(
    IDbContext context
) : ICommandHandler<UpdateHealthQuestionCommand, UpdateHealthQuestionResponse>
{
    public async Task<Result<UpdateHealthQuestionResponse>> Handle(UpdateHealthQuestionCommand command, CancellationToken cancellationToken)
    {
        var question = await context.HealthQuestions
            .FirstOrDefaultAsync(q => q.QuestionId == command.QuestionId, cancellationToken);

        if (question == null)
        {
            return Result.Failure<UpdateHealthQuestionResponse>(
                Error.NotFound("HealthQuestion.NotFound", $"Health question with ID '{command.QuestionId}' not found."));
        }

        question.Content = command.Content;
        question.IsRequired = command.IsRequired;
        question.QuestionType = command.QuestionType;

        await context.SaveChangesAsync(cancellationToken);

        return new UpdateHealthQuestionResponse
        {
            QuestionId = question.QuestionId,
            Content = question.Content,
            IsRequired = question.IsRequired,
            QuestionType = question.QuestionType.ToString()
        };
    }
}