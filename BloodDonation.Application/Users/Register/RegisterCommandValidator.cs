using FluentValidation;

namespace BloodDonation.Application.Users.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
        RuleFor(x => x.DateOfBirth).NotEmpty();
        RuleFor(x => x.Phone).NotEmpty();
        RuleFor(x => x.Address).NotEmpty();
    }
}