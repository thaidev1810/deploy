using BloodDonation.Application.Abstraction.Data;
using BloodDonation.Application.Abstraction.Messaging;
using BloodDonation.Domain.Common;
using BloodDonation.Domain.QuestionForm;
using BloodDonation.Domain.QuestionForm.Errors;
using Microsoft.EntityFrameworkCore;

namespace BloodDonation.Application.QuestionForm.CreateHealthQuestion;

public class CreateHealthQuestionCommandHandler(
    IDbContext context
) : ICommandHandler<CreateHealthQuestionCommand, CreateHealthQuestionResponse>
{
    public async Task<Result<CreateHealthQuestionResponse>> Handle(CreateHealthQuestionCommand command, CancellationToken cancellationToken)
    {
        // Validate QuestionType (nếu muốn, hoặc đã validate ở validator)
        if (!Enum.IsDefined(typeof(QuestionType), command.QuestionType))
        {
            return Result.Failure<CreateHealthQuestionResponse>(QuestionFormErrors.InvalidQuestionType);
        }

        // Validate Content không rỗng
        if (string.IsNullOrWhiteSpace(command.Content))
        {
            return Result.Failure<CreateHealthQuestionResponse>(QuestionFormErrors.ContentEmpty);
        }

        var newQuestion = new HealthQuestion
        {
            QuestionId = Guid.NewGuid(),
            Content = command.Content,
            IsRequired = command.IsRequired,
            QuestionType = command.QuestionType
        };

        context.HealthQuestions.Add(newQuestion);
        await context.SaveChangesAsync(cancellationToken);

        var response = new CreateHealthQuestionResponse
        {
            QuestionId = newQuestion.QuestionId,
            Content = newQuestion.Content,
            IsRequired = newQuestion.IsRequired,
            QuestionType = newQuestion.QuestionType.ToString()
        };

        return response;
    }
}