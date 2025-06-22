using BloodDonation.Domain.QuestionForm;

namespace BloodDonation.Application.QuestionForm.GetUserHealthFormDetail
{
    public class GetUserHealthFormDetailResponse
    {
        public Guid FormId { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public List<QuestionAnswerDto> Questions { get; set; } = new List<QuestionAnswerDto>();
        
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; }
        public Guid? ApprovedBy { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public string? ApprovedByStaffName { get; set; }
        public int TotalQuestions { get; set; }
    }
    public class QuestionAnswerDto
    {
        public string Content { get; set; } = null!;
        public string Answer { get; set; } = null!;
    }
}

