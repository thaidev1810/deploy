using FluentValidation;
namespace BloodDonation.Application.BlogPosts.CreateBlogPost;

public class CreateBlogPostCommandValidator : AbstractValidator<CreateBlogPostCommand>
{
    public CreateBlogPostCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required");
        
        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Content is required");
    } 
}