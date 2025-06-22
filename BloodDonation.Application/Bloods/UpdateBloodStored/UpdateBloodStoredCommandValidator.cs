using FluentValidation;

namespace BloodDonation.Application.Bloods.UpdateBloodStored;

public class UpdateBloodStoredCommandValidator : AbstractValidator<UpdateBloodStoredCommand>
{
    public UpdateBloodStoredCommandValidator()
    {
        RuleFor(x => x.BloodTypeName).NotEmpty().WithMessage("BloodTypeName is required.");
        RuleFor(x => x.Quantity).NotNull().WithMessage("QuantityDelta is required.");
    }
}