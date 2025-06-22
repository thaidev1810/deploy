using BloodDonation.Application.Abstraction.Messaging;
using BloodDonation.Application.Abstraction.Query;
using BloodDonation.Domain.Common;

namespace BloodDonation.Application.QuestionForm.GetHealthQuestion;

public class GetHealthQuestionQuery : IPageableQuery, IQuery<Page<GetHealthQuestionResponse>>
{
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
}