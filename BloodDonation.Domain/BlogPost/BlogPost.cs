using BloodDonation.Domain.Users;

namespace BloodDonation.Domain.BlogPost;

public class BlogPost
{
    public Guid PostId { get; set; }
    public Guid UserId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime PublishedDate { get; set; }
    
    public User? User { get; set; }

}