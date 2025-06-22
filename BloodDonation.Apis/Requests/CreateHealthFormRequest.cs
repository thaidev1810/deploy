namespace BloodDonation.Apis.Requests;

public class CreateHealthFormRequest
{
    public List<HealthAnswerRequest> Answers { get; set; } = new();
}
public class HealthAnswerRequest
{
    public Guid QuestionId { get; set; }
    public string Answer { get; set; } = null!;
}