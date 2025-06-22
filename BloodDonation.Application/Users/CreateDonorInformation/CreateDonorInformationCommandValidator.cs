using System.Data;
using FluentValidation;

namespace BloodDonation.Application.Users.CreateDonorInformation;

public class CreateDonorInformationCommandValidator : AbstractValidator<CreateDonorInformationCommand>
{
    public CreateDonorInformationCommandValidator()
    {
        RuleFor(command => command.Weight).NotNull().NotEmpty();
        RuleFor(command => command.Height).NotNull().NotEmpty();
    }
}