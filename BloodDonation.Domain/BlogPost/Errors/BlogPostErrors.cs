using BloodDonation.Domain.Common;

namespace BloodDonation.Domain.BlogPost.Errors;

public class BlogPostErrors
{
    public static readonly Error NotFound = Error.NotFound(
        "BlogPost.NotFound",
        "The specified blog post does not exist.");
    
    public static readonly Error TitleEmpty = Error.Failure(
        "BlogPost.TitleEmpty",
        "The blog post title cannot be empty.");
    
    public static readonly Error ContentEmpty = Error.Failure(
        "BlogPost.ContentEmpty",
        "The blog post content cannot be empty.");
    
    public static readonly Error PublishedDateInvalid = Error.Failure(
        "BlogPost.PublishedDateInvalid",
        "The published date is invalid.");
    
    public static Error BlogPostNotFound(Guid postId) =>
        Error.NotFound(
            "BlogPost.NotFound",
            $"No blog post found with ID '{postId}'."
        );
    public static Error UserNotFound(Guid userId) =>
        Error.NotFound(
            "UserInBlog.NotFound",
            $"No User found with ID '{userId}'."
        );
}