using BloodDonation.Apis.Extensions;
using BloodDonation.Apis.Requests;
using BloodDonation.Application.QuestionForm.GetHealthFormForStaff;
using BloodDonation.Application.QuestionForm.GetUserHealthFormDetail;
using BloodDonation.Application.QuestionForm.UpdateHealthFormForStaff;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace BloodDonation.Apis.Controller;

[Route("api/")]
[ApiController]
public class HealthFormController : ControllerBase
{
    private readonly ISender _mediator;

    public HealthFormController(ISender mediator)
    {
        _mediator = mediator;
    }

    [Authorize(Roles = "Staff")]
    [HttpGet("healthform/get-healthforms-for-staff")]
    public async Task<IResult> GetHealthFormsForStaff(
        [FromQuery] int pageNumber,
        [FromQuery] int pageSize,
        [FromQuery] string? status,
        CancellationToken cancellationToken)
    {
        var query = new GetHealthFormForStaffQuery()
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            Status = status
        };

        var result = await _mediator.Send(query, cancellationToken);

        return result.MatchOk();
    }


    
    [Authorize(Roles = "Staff")]
    [HttpGet("healthform/get-user-healthform/{formId:guid}")]
    public async Task<IResult> GetUserHealthFormDetail(Guid formId,[FromQuery] int pageNumber, [FromQuery] int pageSize, CancellationToken cancellationToken)
    {
        var query = new GetUserHealthFormDetailQuery()
        {
            FormId = formId,
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var result = await _mediator.Send(query, cancellationToken);
        return result.MatchOk();
    }
    [Authorize(Roles = "Staff")]
    [HttpPut("healthform/update-user-healthform/{formId:guid}")]
    public async Task<IResult> UpdateHealthFormForStaff(Guid formId, [FromBody] UpdateHealthFormForStaffRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateHealthFormForStaffCommand()
        {
            FormId = formId,
            Status = request.Status
        };

        var result = await _mediator.Send(command, cancellationToken);

        return result.MatchOk();
    }
}