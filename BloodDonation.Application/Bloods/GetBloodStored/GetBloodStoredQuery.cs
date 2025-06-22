using BloodDonation.Application.Abstraction.Messaging;
using BloodDonation.Application.Abstraction.Query;
using BloodDonation.Domain.Common;

namespace BloodDonation.Application.Bloods.GetBloodStored;

public class GetBloodStoredQuery : IPageableQuery, IQuery<Page<GetBloodStoredResponse>>
{
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
}