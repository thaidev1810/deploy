using BloodDonation.Apis.Extensions;
using BloodDonation.Application.BloodDonation.ConfirmDonationMatch;
using BloodDonation.Application.BloodDonation.CreateDonationMatch;
using BloodDonation.Application.BloodDonation.CreateDonationRequest;
using BloodDonation.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonation.Apis.Controller;

[Route("api/")]
[ApiController]
public class BloodDonationController : ControllerBase
{
    private readonly ISender _mediator;

    public BloodDonationController(ISender mediator)
    {
        _mediator = mediator;
    }
    
    [Authorize]
    [HttpPost("create-request")]
    public async Task<IResult> CreateDonationRequest([FromBody] CreateDonationRequestCommand command, CancellationToken cancellationToken)
    {
        Result<CreateDonationRequestResponse> result = await _mediator.Send(command, cancellationToken);
        return result.MatchCreated(id => $"/blood-donation/request/{id}");
    }

    [Authorize(Roles = "Staff")]
    [HttpPost("match-donor")]
    public async Task<IResult> CreateDonationMatch([FromBody] CreateDonationMatchCommand command, CancellationToken cancellationToken)
    {
        Result<CreateDonationMatchResponse> result = await _mediator.Send(command, cancellationToken);
        return result.MatchCreated(id => $"/blood-donation/match/{id}");
    }

    [Authorize]
    [HttpPut("confirm-match")]
    public async Task<IResult> ConfirmDonationMatch([FromBody] ConfirmDonationMatchCommand command, CancellationToken cancellationToken)
    {
        Result result = await _mediator.Send(command, cancellationToken);
        return result.MatchOk();
    }
    
}