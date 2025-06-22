using BloodDonation.Application.Abstraction.Messaging;
using BloodDonation.Domain.QuestionForm;

namespace BloodDonation.Application.QuestionForm.UpdateHealthQuestion;

public class UpdateHealthQuestionCommand : ICommand<UpdateHealthQuestionResponse>
{
    public Guid QuestionId { get; set; }
    public string Content { get; set; } = null!;
    public bool IsRequired { get; set; }
    public QuestionType QuestionType { get; set; }
}