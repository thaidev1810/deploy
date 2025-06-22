using BloodDonation.Application.Abstraction.Authentication;
using BloodDonation.Application.Abstraction.Data;
using BloodDonation.Application.Abstraction.Messaging;
using BloodDonation.Domain.BlogPost;
using BloodDonation.Domain.BlogPost.Errors;
using BloodDonation.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace BloodDonation.Application.BlogPosts.CreateBlogPost;

public class CreateBlogPostCommandHandler(
    IDbContext context,
    IUserContext userContext
) : ICommandHandler<CreateBlogPostCommand, CreateBlogPostResponse>
{
    public async Task<Result<CreateBlogPostResponse>> Handle(CreateBlogPostCommand command, CancellationToken cancellationToken)
    {
        var userId = userContext.UserId;
        
        // Kiểm tra user có tồn tại không
        var userExists = await context.Users.AnyAsync(u => u.UserId == userId, cancellationToken);
        if (!userExists)
        {
            return Result.Failure<CreateBlogPostResponse>(BlogPostErrors.UserNotFound(userId));
        }

        var blogPost = new BlogPost()
        {
            PostId = Guid.NewGuid(),
            UserId = userId,
            Title = command.Title,
            Content = command.Content,
            PublishedDate = DateTime.UtcNow
        };

        context.BlogPosts.Add(blogPost);
        await context.SaveChangesAsync(cancellationToken);

        return new CreateBlogPostResponse
        {
            PostId = blogPost.PostId,
            UserId = blogPost.UserId,
            Title = blogPost.Title,
            Content = blogPost.Content,
            PublishedDate = blogPost.PublishedDate
        };
    }
}