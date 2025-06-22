using FluentValidation;

namespace BloodDonation.Application.Bloods.CheckBloodCompatibility;

public class CheckBloodCompatibilityQueryValidator : AbstractValidator<CheckBloodCompatibilityQuery>
{
    public CheckBloodCompatibilityQueryValidator()
    {
        RuleFor(x => x.FromBloodType).NotEmpty();
        RuleFor(x => x.ToBloodType).NotEmpty();
        RuleFor(x => x.ComponentType).IsInEnum();
    }
}