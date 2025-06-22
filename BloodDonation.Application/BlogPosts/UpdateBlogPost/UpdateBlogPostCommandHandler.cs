using BloodDonation.Application.Abstraction.Authentication;
using BloodDonation.Application.Abstraction.Data;
using BloodDonation.Application.Abstraction.Messaging;
using BloodDonation.Domain.BlogPost;
using BloodDonation.Domain.BlogPost.Errors;
using BloodDonation.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace BloodDonation.Application.BlogPosts.UpdateBlogPost;

public class UpdateBlogPostCommandHandler(
    IDbContext context,
    IUserContext userContext
) : ICommandHandler<UpdateBlogPostCommand, UpdateBlogPostResponse>
{
    public async Task<Result<UpdateBlogPostResponse>> Handle(UpdateBlogPostCommand command, CancellationToken cancellationToken)
    {
        var userId = userContext.UserId;

        // Tìm bài viết theo UserId và Title (hoặc có thể cần thêm PostId nếu có)
        var blogPost = await context.BlogPosts
            .FirstOrDefaultAsync(bp => bp.UserId == userId && bp.PostId == command.PostId, cancellationToken);

        if (blogPost == null)
        {
            return Result.Failure<UpdateBlogPostResponse>(BlogPostErrors.NotFound);
        }

        // Cập nhật nội dung và ngày xuất bản
        blogPost.Title = command.Title;
        blogPost.Content = command.Content;
        blogPost.PublishedDate = DateTime.UtcNow;

        await context.SaveChangesAsync(cancellationToken);

        return new UpdateBlogPostResponse
        {
            PostId = blogPost.PostId,
            UserId = blogPost.UserId,
            Title = blogPost.Title,
            Content = blogPost.Content,
            PublishedDate = blogPost.PublishedDate
        };
    }
}