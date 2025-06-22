using BloodDonation.Application.Abstraction.Messaging;
using BloodDonation.Domain.QuestionForm;
using BloodDonation.Domain.Common;

namespace BloodDonation.Application.QuestionForm.GetQuestionType;

public class GetQuestionTypeQueryHandler : IQueryHandler<GetQuestionTypeQuery, List<GetQuestionTypeResponse>>
{
    public Task<Result<List<GetQuestionTypeResponse>>> Handle(GetQuestionTypeQuery request, CancellationToken cancellationToken)
    {
        var values = Enum.GetValues(typeof(QuestionType))
            .Cast<QuestionType>()
            .Select(qt => new GetQuestionTypeResponse
            {
                Id = (int)qt,
                Name = qt.ToString()
            })
            .ToList();

        return Task.FromResult(Result.Success(values));
    }
}