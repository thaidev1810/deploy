using BloodDonation.Application.Abstraction.Messaging;

namespace BloodDonation.Application.BlogPosts.UpdateBlogPost;

public class UpdateBlogPostCommand : ICommand<UpdateBlogPostResponse>
{
    public Guid PostId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
}