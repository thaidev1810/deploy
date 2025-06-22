namespace BloodDonation.Application.BlogPosts.UpdateBlogPost;

public class UpdateBlogPostResponse
{
    public Guid PostId { get; set; }
    public Guid UserId { get; set; }
    public string Title { get; set; } = default!;
    public string Content { get; set; } = default!;
    public DateTime PublishedDate { get; set; }
}