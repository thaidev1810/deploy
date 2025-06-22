using BloodDonation.Application.Abstraction.Messaging;
using BloodDonation.Application.Abstraction.Query;
using BloodDonation.Domain.Common;

namespace BloodDonation.Application.Users.GetDonorInformation;

public class GetDonorInformationQuery : IPageableQuery, IQuery<Page<GetDonorInformationResponse>>
{
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
}