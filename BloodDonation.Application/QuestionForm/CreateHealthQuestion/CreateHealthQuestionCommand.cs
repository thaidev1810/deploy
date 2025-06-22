using BloodDonation.Application.Abstraction.Messaging;
using BloodDonation.Domain.QuestionForm;

namespace BloodDonation.Application.QuestionForm.CreateHealthQuestion;

public class CreateHealthQuestionCommand : ICommand<CreateHealthQuestionResponse>
{
    public string Content { get; set; }
    public bool IsRequired { get; set; }
    public QuestionType QuestionType { get; set; }
}