using FluentValidation;

namespace BloodDonation.Application.Users.CreateHealthForm;

public class CreateHealthFormCommandValidator : AbstractValidator<CreateHealthFormCommand>
{
    public CreateHealthFormCommandValidator()
    {

        RuleFor(x => x.Answers)
            .NotNull().WithMessage("Answers list must not be null.")
            .NotEmpty().WithMessage("At least one answer is required.");

        RuleForEach(x => x.Answers).SetValidator(new HealthAnswerCommandValidator());
    }
}

public class HealthAnswerCommandValidator : AbstractValidator<HealthAnswerCommand>
{
    public HealthAnswerCommandValidator()
    {
        RuleFor(x => x.QuestionId)
            .NotEmpty().WithMessage("QuestionId is required.");

        RuleFor(x => x.Answer)
            .NotNull().WithMessage("Answer must not be null.")
            .NotEmpty().WithMessage("Answer is required.")
            .MaximumLength(1000).WithMessage("Answer must not exceed 1000 characters.");
    }
}