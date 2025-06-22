using BloodDonation.Application.Abstraction.Messaging;
using BloodDonation.Application.Abstraction.Query;
using BloodDonation.Domain.Common;

namespace BloodDonation.Application.BlogPosts.GetBlogPost;

public class GetBlogPostQuery : IPageableQuery, IQuery<Page<GetBlogPostResponse>>
{
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
}