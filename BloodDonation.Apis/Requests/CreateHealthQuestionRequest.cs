using BloodDonation.Domain.QuestionForm;

namespace BloodDonation.Apis.Requests;

public class CreateHealthQuestionRequest
{
    public string Content { get; set; }
    public bool IsRequired { get; set; }
    public QuestionType QuestionType { get; set; }
}