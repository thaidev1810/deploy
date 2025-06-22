using BloodDonation.Application.Abstraction.Messaging;

namespace BloodDonation.Application.BlogPosts.DeleteBlogPost;

public class DeleteBlogPostCommand : ICommand<DeleteBlogPostResponse>
{
    public Guid PostId { get; set; }
}