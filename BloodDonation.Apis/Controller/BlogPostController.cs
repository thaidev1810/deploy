using BloodDonation.Application.BlogPosts.CreateBlogPost;
using BloodDonation.Apis.Requests;
using BloodDonation.Apis.Extensions;
using BloodDonation.Application.BlogPosts.DeleteBlogPost;
using BloodDonation.Application.BlogPosts.GetBlogPost;
using BloodDonation.Application.BlogPosts.UpdateBlogPost;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonation.Apis.Controller;

[Route("api/")]
[ApiController]
public class BlogPostController : ControllerBase
{
    private readonly ISender _mediator;

    public BlogPostController(ISender mediator)
    {
        _mediator = mediator;
    }
    [Authorize]
    [HttpPost("blogpost/create-blogpost")]
    public async Task<IResult> CreateBlogPost([FromBody] CreateBlogPostRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateBlogPostCommand
        {
            Title = request.Title,
            Content = request.Content
        };

        var result = await _mediator.Send(command, cancellationToken);
        return result.MatchCreated(id => $"/blogpost/{id}");
    }
    [HttpGet("blogpost/get-blogpost")]
    public async Task<IResult> GetBlogPosts([FromQuery] int pageNumber, [FromQuery] int pageSize, CancellationToken cancellationToken)
    {
        var query = new GetBlogPostQuery()
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var result = await _mediator.Send(query, cancellationToken);
        return result.MatchOk();
    }
    [Authorize]
    [HttpPut("blogpost/update/{id:guid}")]
    public async Task<IResult> UpdateBlogPost(Guid id, [FromBody] UpdateBlogPostRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateBlogPostCommand()
        {
            PostId = id,
            Title = request.Title,
            Content = request.Content
        };

        var result = await _mediator.Send(command, cancellationToken);
        return result.MatchOk();
    }
    [Authorize]
    [HttpDelete("blogpost/delete/{id:guid}")]
    public async Task<IResult> DeleteBlogPost(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteBlogPostCommand()
        {
            PostId = id
        };

        var result = await _mediator.Send(command, cancellationToken);
        return result.MatchOk();
    }
}