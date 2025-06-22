using BloodDonation.Application.Abstraction.Messaging;
using BloodDonation.Application.Abstraction.Query;

namespace BloodDonation.Application.Users.GetHealthForm;

public class GetHealthFormQuery :IPageableQuery ,IQuery<GetHealthFormResponse>
{
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
}