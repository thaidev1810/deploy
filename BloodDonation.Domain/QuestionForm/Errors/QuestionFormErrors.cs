using BloodDonation.Domain.Common;

namespace BloodDonation.Domain.QuestionForm.Errors;

public static class QuestionFormErrors
{
    // HealthForm related errors
    public static readonly Error FormNotFound = Error.NotFound(
        "HealthForm.NotFound",
        "Health form not found.");

    public static Error UserNotFound(Guid userId) =>
        Error.NotFound(
            "HealthForm.UserNotFound",
            $"User with ID '{userId}' was not found.");

    public static Error QuestionNotFound(IEnumerable<Guid> questionIds)
    {
        var ids = string.Join(", ", questionIds);
        return Error.NotFound(
            "HealthQuestion.NotFound",
            $"Health questions with IDs [{ids}] were not found.");
    }
    
    public static readonly Error AlreadyApproved = Error.Failure(
        "HealthForm.AlreadyApproved",
        "Health form has already been approved.");

    public static readonly Error NotApprovedYet = Error.Failure(
        "HealthForm.NotApprovedYet",
        "Health form has not been approved yet.");

    public static readonly Error InvalidApproval = Error.Failure(
        "HealthForm.InvalidApproval",
        "Invalid approval operation.");
    public static readonly Error HealthFormExist = Error.Failure(
        "HealthForm.InvalidHealthForm",
        "This user already has a health form.");
    public static readonly Error HealthFormNotFound = Error.Failure(
        "HealthForm.Notfound",
        "Health form not found for current user.");
    public static Error InvalidStatus(string status) =>
        Error.Failure(
            "InvalidStatus",
            $"Status '{status}' is not valid.");

    public static readonly Error ContentEmpty = Error.Failure(
        "HealthQuestion.ContentEmpty",
        "Question content cannot be empty.");

    public static readonly Error InvalidQuestionType = Error.Failure(
        "HealthQuestion.InvalidQuestionType",
        "Invalid question type.");

    public static readonly Error RequiredQuestionNotAnswered = Error.Failure(
        "HealthQuestion.RequiredNotAnswered",
        "Required question has not been answered.");

    // HealthAnswer related errors
    public static readonly Error AnswerNotFound = Error.NotFound(
        "HealthAnswer.NotFound",
        "Health answer not found.");

    public static readonly Error AnswerEmpty = Error.Failure(
        "HealthAnswer.Empty",
        "Answer cannot be empty.");

    public static readonly Error InvalidAnswerFormat = Error.Failure(
        "HealthAnswer.InvalidFormat",
        "Answer format is invalid.");

    public static Error QuestionMismatch(Guid questionId) =>
        Error.Failure(
            "HealthAnswer.QuestionMismatch",
            $"Answer does not match the question with ID '{questionId}'.");
    
}
