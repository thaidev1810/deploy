using BloodDonation.Application.Abstraction.Data;
using BloodDonation.Application.Abstraction.Messaging;
using BloodDonation.Domain.Common;
using BloodDonation.Domain.QuestionForm.Errors;
using Microsoft.EntityFrameworkCore;

namespace BloodDonation.Application.QuestionForm.DeleteHealthQuestion;

public class DeleteHealthQuestionCommandHandler(
    IDbContext context
) : ICommandHandler<DeleteHealthQuestionCommand, DeleteHealthQuestionResponse>
{
    public async Task<Result<DeleteHealthQuestionResponse>> Handle(DeleteHealthQuestionCommand command, CancellationToken cancellationToken)
    {
        var question = await context.HealthQuestions
            .FirstOrDefaultAsync(q => q.QuestionId == command.QuestionId, cancellationToken);

        if (question == null)
        {
            return Result.Failure<DeleteHealthQuestionResponse>(
                Error.NotFound("HealthQuestion.NotFound", $"Health question with ID '{command.QuestionId}' not found."));
        }

        context.HealthQuestions.Remove(question);
        await context.SaveChangesAsync(cancellationToken);

        return new DeleteHealthQuestionResponse
        {
            QuestionId = question.QuestionId
        };
    }
}