using BloodDonation.Application.Abstraction.Query;
using BloodDonation.Domain.Common;
using BloodDonation.Application.Abstraction.Messaging;

namespace BloodDonation.Application.Users.GetUser;

public class GetUsersQuery : IPageableQuery, IQuery<Page<GetUsersResponse>>
{
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
}