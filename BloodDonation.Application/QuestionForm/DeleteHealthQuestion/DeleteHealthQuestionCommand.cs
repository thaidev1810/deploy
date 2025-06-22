using BloodDonation.Application.Abstraction.Messaging;

namespace BloodDonation.Application.QuestionForm.DeleteHealthQuestion;

public class DeleteHealthQuestionCommand : ICommand<DeleteHealthQuestionResponse>
{
    public Guid QuestionId { get; set; }
}