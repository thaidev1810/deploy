namespace BloodDonation.Application.BlogPosts.CreateBlogPost;

public class CreateBlogPostResponse
{
    public Guid PostId { get; set; }
    public Guid UserId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime PublishedDate { get; set; }
}