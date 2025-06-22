using BloodDonation.Domain.QuestionForm;

namespace BloodDonation.Application.QuestionForm.GetHealthFormForStaff;

public class GetHealthFormForStaffResponse
{
    public Guid FormId { get; set; }
    public Guid UserId { get; set; }
    public DateTime CreatedAt { get; set; }

    public string Status { get; set; }
    public string? ApprovedByStaffName { get; set; }
}