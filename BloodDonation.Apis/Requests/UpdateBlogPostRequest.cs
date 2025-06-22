namespace BloodDonation.Apis.Requests;

public class UpdateBlogPostRequest
{
    public string Title { get; set; } = default!;
    public string Content { get; set; }= default!;
}