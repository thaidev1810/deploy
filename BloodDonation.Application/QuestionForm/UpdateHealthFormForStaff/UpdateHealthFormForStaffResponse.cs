using BloodDonation.Domain.QuestionForm;

namespace BloodDonation.Application.QuestionForm.UpdateHealthFormForStaff;

public class UpdateHealthFormForStaffResponse
{
    public Guid FormId { get; set; }
    public Guid UserId { get; set; }
    public string Status { get; set; }
    public Guid? ApprovedBy { get; set; }
    public DateTime? ApprovedAt { get; set; }
    public string? ApprovedByStaffName { get; set; }
}