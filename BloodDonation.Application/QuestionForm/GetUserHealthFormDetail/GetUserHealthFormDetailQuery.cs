using BloodDonation.Application.Abstraction.Messaging;
using BloodDonation.Application.Abstraction.Query;

namespace BloodDonation.Application.QuestionForm.GetUserHealthFormDetail;

public class GetUserHealthFormDetailQuery : IPageableQuery ,IQuery<GetUserHealthFormDetailResponse>
{
    public Guid FormId { get; set; }
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
}