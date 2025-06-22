using BloodDonation.Domain.QuestionForm;

namespace BloodDonation.Application.QuestionForm.CreateHealthQuestion;

public class CreateHealthQuestionResponse
{
    public Guid QuestionId { get; set; }
    public string Content { get; set; }
    public bool IsRequired { get; set; }
    public string QuestionType { get; set; }
}