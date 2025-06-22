using BloodDonation.Application.Abstraction.Messaging;
using BloodDonation.Application.Abstraction.Query;
using BloodDonation.Application.Bloods.GetBloodStored;
using BloodDonation.Domain.Common;

namespace BloodDonation.Application.Bloods.GetBloodType;

public class GetBloodTypeQuery : IPageableQuery, IQuery<Page<GetBloodTypeResponse>>
{
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
}