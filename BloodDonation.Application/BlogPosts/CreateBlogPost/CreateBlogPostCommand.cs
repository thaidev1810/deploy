using BloodDonation.Application.Abstraction.Messaging;

namespace BloodDonation.Application.BlogPosts.CreateBlogPost;

public class CreateBlogPostCommand : ICommand<CreateBlogPostResponse>
{
    public string Title { get; set; }
    public string Content { get; set; }
}