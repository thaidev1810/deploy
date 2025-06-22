using FluentValidation;
using BloodDonation.Domain.QuestionForm;

namespace BloodDonation.Application.QuestionForm.CreateHealthQuestion;

public class CreateHealthQuestionCommandValidator : AbstractValidator<CreateHealthQuestionCommand>
{
    public CreateHealthQuestionCommandValidator()
    {
        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Content is required.")
            .MaximumLength(500).WithMessage("Content must not exceed 500 characters.");

        RuleFor(x => x.QuestionType)
            .IsInEnum().WithMessage("Invalid question type.")
            .Must(qt => qt == QuestionType.Text 
                        || qt == QuestionType.Checkbox 
                        || qt == QuestionType.Radio 
                        || qt == QuestionType.Dropdown)
            .WithMessage("Question type must be one of Text, Checkbox, Radio, or Dropdown.");

        // IsRequired is bool, no validation needed unless you want to enforce something specific
    }
}