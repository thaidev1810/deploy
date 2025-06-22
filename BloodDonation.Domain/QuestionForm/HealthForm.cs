using BloodDonation.Domain.Users;

namespace BloodDonation.Domain.QuestionForm;

public class HealthForm
{
    public Guid FormId { get; set; }
    public Guid UserId { get; set; }
    public DateTime CreatedAt { get; set; }

    public FormStatus Status { get; set; }
    public Guid? ApprovedBy { get; set; }
    public DateTime? ApprovedAt { get; set; }
    public string? ApprovedByStaffName { get; set; }

    // Navigation
    public User User { get; set; } = null!;
    public User? ApprovedByStaff { get; set; }
    public ICollection<HealthAnswer> Answers { get; set; } = new List<HealthAnswer>();
}
