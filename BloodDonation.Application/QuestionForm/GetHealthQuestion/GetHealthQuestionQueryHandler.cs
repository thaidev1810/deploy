using BloodDonation.Application.Abstraction.Data;
using BloodDonation.Application.Abstraction.Messaging;
using BloodDonation.Application.Abstraction.Query;
using BloodDonation.Domain.Common;
using BloodDonation.Domain.QuestionForm;
using Microsoft.EntityFrameworkCore;

namespace BloodDonation.Application.QuestionForm.GetHealthQuestion;

public class GetHealthQuestionQueryHandler(IDbContext context) : IQueryHandler<GetHealthQuestionQuery, Page<GetHealthQuestionResponse>>
{
    public async Task<Result<Page<GetHealthQuestionResponse>>> Handle(GetHealthQuestionQuery request, CancellationToken cancellationToken)
    {
        var query = context.HealthQuestions.AsNoTracking();

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .ApplyPagination(request.PageNumber, request.PageSize)
            .Select(hq => new GetHealthQuestionResponse
            {
                QuestionId = hq.QuestionId,
                Content = hq.Content,
                IsRequired = hq.IsRequired,
                QuestionType = hq.QuestionType.ToString()
            })
            .ToListAsync(cancellationToken);

        var page = new Page<GetHealthQuestionResponse>(
            items,
            totalCount,
            request.PageNumber,
            request.PageSize);

        return Result.Success(page);
    }
}