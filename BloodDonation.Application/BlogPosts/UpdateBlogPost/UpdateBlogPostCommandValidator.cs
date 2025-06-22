using FluentValidation;

namespace BloodDonation.Application.BlogPosts.UpdateBlogPost;

public class UpdateBlogPostCommandValidator : AbstractValidator<UpdateBlogPostCommand>
{
    public UpdateBlogPostCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.");
        RuleFor(x => x.Content).NotEmpty().WithMessage("Content is required.");
    }
}