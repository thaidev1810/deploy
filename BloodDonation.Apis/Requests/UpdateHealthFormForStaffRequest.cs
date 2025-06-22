using BloodDonation.Domain.QuestionForm;

namespace BloodDonation.Apis.Requests;

public class UpdateHealthFormForStaffRequest
{
    public FormStatus Status { get; set; }
}