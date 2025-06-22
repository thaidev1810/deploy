using FluentValidation;
using BloodDonation.Domain.QuestionForm;

namespace BloodDonation.Application.QuestionForm.UpdateHealthQuestion;

public class UpdateHealthQuestionCommandValidator : AbstractValidator<UpdateHealthQuestionCommand>
{
    public UpdateHealthQuestionCommandValidator()
    {
        
        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Content is required.")
            .MaximumLength(1000).WithMessage("Content cannot exceed 1000 characters.");

        RuleFor(x => x.QuestionType)
            .IsInEnum().WithMessage("Invalid QuestionType value.");
        
        // IsRequired is bool, no need to validate nullability
    }
}