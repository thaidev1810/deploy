using BloodDonation.Domain.QuestionForm;
using FluentValidation;

namespace BloodDonation.Application.QuestionForm.UpdateHealthFormForStaff;

public class UpdateHealthFormForStaffCommandValidator : AbstractValidator<UpdateHealthFormForStaffCommand>
{
    public UpdateHealthFormForStaffCommandValidator()
    {

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Invalid status.")
            .Must(status => status == FormStatus.Pending || status == FormStatus.Cancelled || status == FormStatus.Approved)
            .WithMessage("Status must be Pending, Cancelled or Approved.");
    }
}