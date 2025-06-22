using BloodDonation.Application.Abstraction.Messaging;
using BloodDonation.Domain.QuestionForm;

namespace BloodDonation.Application.QuestionForm.UpdateHealthFormForStaff;

public class UpdateHealthFormForStaffCommand : ICommand<UpdateHealthFormForStaffResponse>
{
    public Guid FormId { get; set; }
    public FormStatus Status { get; set; }
}