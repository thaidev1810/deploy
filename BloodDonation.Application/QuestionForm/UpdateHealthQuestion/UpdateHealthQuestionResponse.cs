using BloodDonation.Domain.QuestionForm;

namespace BloodDonation.Application.QuestionForm.UpdateHealthQuestion;

public class UpdateHealthQuestionResponse
{
    public Guid QuestionId { get; set; }
    public string Content { get; set; } = null!;
    public bool IsRequired { get; set; }
    public string QuestionType { get; set; }
}