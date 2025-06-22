using BloodDonation.Apis.Extensions;
using BloodDonation.Apis.Requests;
using BloodDonation.Application.Bloods.CheckBloodCompatibility;
using BloodDonation.Application.Bloods.GetBloodStored;
using BloodDonation.Application.Bloods.GetBloodType;
using BloodDonation.Application.Bloods.UpdateBloodStored;
using BloodDonation.Domain.Bloods;
using BloodDonation.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonation.Apis.Controller;

[Route("api/")]
[ApiController]
public class BloodController
    : ControllerBase
{
    private readonly ISender _mediator;

    public BloodController(ISender mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("blood/get-blood-stored")]
    public async Task<IResult> GetBloodStored([FromQuery] int pageNumber, [FromQuery] int pageSize, CancellationToken cancellation)
    {
        Result<Page<GetBloodStoredResponse>> result = await _mediator.Send(new GetBloodStoredQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
        }, cancellation);
        return result.MatchOk();
    }
    
    [HttpPut("blood/update-blood-stored")]
    public async Task<IResult> UpdateQuantity([FromBody] UpdateBloodStoredRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateBloodStoredCommand
        {
            // StoredId = request.StoredId,
            BloodTypeName = request.BloodTypeName,
            Quantity = request.Quantity
        };

        var result = await _mediator.Send(command, cancellationToken);
        return result.MatchOk();
    }
    
    [HttpGet("blood/get-blood-Type")]
    public async Task<IResult> GetBloodType([FromQuery] int pageNumber, [FromQuery] int pageSize, CancellationToken cancellation)
    {
        Result<Page<GetBloodTypeResponse>> result = await _mediator.Send(new GetBloodTypeQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
        }, cancellation);
        return result.MatchOk();
    }
    
    [HttpGet("blood/check-compatibility")]
    public async Task<IResult> CheckCompatibility(
        [FromQuery] string from,
        [FromQuery] string to,
        [FromQuery] BloodComponentType componentType,
        CancellationToken cancellationToken)
    {
        var query = new CheckBloodCompatibilityQuery
        {
            FromBloodType = from,
            ToBloodType = to,
            ComponentType = componentType
        };

        var result = await _mediator.Send(query, cancellationToken);
        return result.MatchOk(); 
    }
    
    // [HttpPost("blood/create-blood-stored")]
    // public async Task<IResult> CreateBloodStored([FromBody] CreateBloodStoredRequest request, CancellationToken cancellationToken)
    // {
    //     CreateBloodStoredCommand command = new CreateBloodStoredCommand
    //     {
    //         Quantity = request.Quantity,
    //         BloodTypeName = request.BloodTypeName
    //     };
    //     
    //     Result<CreateBloodStoredResponse> result = await _mediator.Send(command, cancellationToken);
    //     return result.MatchCreated(id => $"/bloodStored/{id}");
    // }
    
}