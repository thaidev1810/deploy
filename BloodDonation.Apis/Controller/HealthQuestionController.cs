using BloodDonation.Application.QuestionForm.CreateHealthQuestion;
using BloodDonation.Apis.Requests;
using BloodDonation.Apis.Extensions;
using BloodDonation.Application.QuestionForm.DeleteHealthQuestion;
using BloodDonation.Application.QuestionForm.GetHealthQuestion;
using BloodDonation.Application.QuestionForm.GetQuestionType;
using BloodDonation.Application.QuestionForm.UpdateHealthQuestion;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonation.Apis.Controller;

[Route("api/")]
[ApiController]
public class HealthQuestionController : ControllerBase
{
    private readonly ISender _mediator;

    public HealthQuestionController(ISender mediator)
    {
        _mediator = mediator;
    }

    [Authorize]
    [HttpPost("healthquestion/create-healthquestion")]
    public async Task<IResult> CreateHealthQuestion([FromBody] CreateHealthQuestionRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateHealthQuestionCommand
        {
            Content = request.Content,
            IsRequired = request.IsRequired,
            QuestionType = request.QuestionType
        };

        var result = await _mediator.Send(command, cancellationToken);

        // Giả sử result là Result<CreateHealthQuestionResponse>
        return result.MatchCreated(id => $"/healthquestion/{id}");
    }
    [HttpGet("healthquestion/get-healthquestions")]
    public async Task<IResult> GetHealthQuestions([FromQuery] int pageNumber, [FromQuery] int pageSize, CancellationToken cancellationToken)
    {
        var query = new GetHealthQuestionQuery()
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var result = await _mediator.Send(query, cancellationToken);
        return result.MatchOk();
    }
    

    [HttpGet("healthquestion/get-questiontypes")]
    public async Task<IResult> GetQuestionTypes(CancellationToken cancellationToken)
    {
        var query = new GetQuestionTypeQuery();
        var result = await _mediator.Send(query, cancellationToken);
        return result.MatchOk();
    }
    
    [Authorize]
    [HttpPut("healthquestion/update/{id:guid}")]
    public async Task<IResult> UpdateHealthQuestion(Guid id, [FromBody] UpdateHealthQuestionRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateHealthQuestionCommand()
        {
            QuestionId = id,
            Content = request.Content,
            IsRequired = request.IsRequired,
            QuestionType = request.QuestionType
        };

        var result = await _mediator.Send(command, cancellationToken);
        return result.MatchOk();
    }
    [Authorize]
    [HttpDelete("healthquestion/delete/{id:guid}")]
    public async Task<IResult> DeleteHealthQuestion(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteHealthQuestionCommand()
        {
            QuestionId = id
        };

        var result = await _mediator.Send(command, cancellationToken);
        return result.MatchOk();
    }
    
}