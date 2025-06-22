using BloodDonation.Application.Abstraction.Messaging;
using BloodDonation.Application.Abstraction.Query;
using BloodDonation.Domain.Common;
using BloodDonation.Domain.QuestionForm;

namespace BloodDonation.Application.QuestionForm.GetHealthFormForStaff;

public class GetHealthFormForStaffQuery : IPageableQuery, IQuery<Page<GetHealthFormForStaffResponse>>
{
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
    
    public string? Status { get; init; }  // thêm property nullable để lọc
}