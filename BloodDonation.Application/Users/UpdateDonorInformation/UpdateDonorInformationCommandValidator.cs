using FluentValidation;

namespace BloodDonation.Application.Users.UpdateDonorInformation;

public class UpdateDonorInformationCommandValidator : AbstractValidator<UpdateDonorInformationCommand>
{
    public UpdateDonorInformationCommandValidator()
    {
        RuleFor(x => x.Weight)
            .GreaterThan(0).WithMessage("Weight must be greater than 0.");

        RuleFor(x => x.Height)
            .GreaterThan(0).WithMessage("Height must be greater than 0.");

        RuleFor(x => x.LastChecked)
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Last checked date cannot be in the future.");
    }
}