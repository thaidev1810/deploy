using BloodDonation.Application.Abstraction.Authentication;
using BloodDonation.Application.Abstraction.Data;
using BloodDonation.Application.Abstraction.Messaging;
using BloodDonation.Domain.BlogPost.Errors;
using BloodDonation.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace BloodDonation.Application.BlogPosts.DeleteBlogPost;

public class DeleteBlogPostCommandHandler(
    IDbContext context,
    IUserContext userContext
) : ICommandHandler<DeleteBlogPostCommand, DeleteBlogPostResponse>
{
    public async Task<Result<DeleteBlogPostResponse>> Handle(DeleteBlogPostCommand command, CancellationToken cancellationToken)
    {
        var userId = userContext.UserId;

        var blogPost = await context.BlogPosts
            .FirstOrDefaultAsync(bp => bp.PostId == command.PostId && bp.UserId == userId, cancellationToken);

        if (blogPost == null)
        {
            return Result.Failure<DeleteBlogPostResponse>(BlogPostErrors.NotFound);
        }

        context.BlogPosts.Remove(blogPost);
        await context.SaveChangesAsync(cancellationToken);

        return new DeleteBlogPostResponse
        {
            Title = blogPost.Title,
            Content = blogPost.Content
        };
    }
}