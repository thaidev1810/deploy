using System.Security.Claims;
using BloodDonation.Apis.Extensions;
using BloodDonation.Apis.Requests;
using BloodDonation.Application.Users.CreateHealthForm;
using BloodDonation.Application.Users.GetHealthForm;
using BloodDonation.Application.Users.CreateDonorInformation;
using BloodDonation.Application.Users.CreateUser;
using BloodDonation.Application.Users.DeleteHealthForm;
using BloodDonation.Application.Users.GetCurrentUser;
using BloodDonation.Application.Users.GetDonorInformation;
using BloodDonation.Application.Users.GetUser;
using BloodDonation.Application.Users.UpdateDonorInformation;
using BloodDonation.Application.Users.UpdateUser;
using BloodDonation.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonation.Apis.Controller;

[Route("api/")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly ISender _mediator;

    public UserController(ISender mediator)
    {
        _mediator = mediator;
    }
    
    // [Authorize(Roles = "Staff")]
    [HttpPost("user/create-user")]
    public async Task<IResult> CreateUser([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
    {
        CreateUserCommand command = new CreateUserCommand
        {
            Email = request.Email,
            Password = request.Password,
            Name = request.Name,
            Role = request.Role,
            DateOfBirth = request.DateOfBirth,
            Gender = request.Gender,
            Address = request.Address,
            Phone = request.Phone
        };
        
        Result<CreateUserCommandResponse> result = await _mediator.Send(command, cancellationToken);
        return result.MatchCreated(id => $"/user/{id}");
    }
    
    // [Authorize(Roles = "Staff")]
    [HttpGet("user/get-users")]
    public async Task<IResult> GetUsers([FromQuery] int pageNumber, [FromQuery] int pageSize, CancellationToken cancellation)
    {
        Result<Page<GetUsersResponse>> result = await _mediator.Send(new GetUsersQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
        }, cancellation);
        return result.MatchOk();
    }
    
    [Authorize]
    [HttpPut("user/update-current-user")]
    public async Task<IResult> UpdateSelf([FromBody] UpdateUserRequest request, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null)
            return Results.Unauthorized();

        var command = new UpdateUserCommand
        {
            UserId = Guid.Parse(userId),
            FullName = request.FullName,
            Email = request.Email,
            Role = null,              
            Status = null,
            BloodType = null,
            IsDonor = null,
            DateOfBirth = request.DateOfBirth,
            Gender = request.Gender,
            Address = request.Address,
            Phone = request.Phone
        };

        var result = await _mediator.Send(command, cancellationToken);
        return result.MatchOk();
    }
    [Authorize]
    [HttpPost("user/create-healthform")]
    public async Task<IResult> CreateHealthForm([FromBody] CreateHealthFormRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateHealthFormCommand()
        {
            // UserId is not set here because handler will get it from IUserContext
            Answers = request.Answers.Select(a => new HealthAnswerCommand
            {
                QuestionId = a.QuestionId,
                Answer = a.Answer
            }).ToList()
        };

        var result = await _mediator.Send(command, cancellationToken);
        return result.MatchCreated(id => $"/healthform/{id}");
    }
    [Authorize]
    [HttpGet("user/get-current-user-healthform")]
    public async Task<IResult> GetHealthForm([FromQuery] int pageNumber, [FromQuery] int pageSize, CancellationToken cancellationToken)
    {
        var query = new GetHealthFormQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        };
        var result = await _mediator.Send(query, cancellationToken);
        return result.MatchOk();
    }
    [Authorize(Roles = "Member")]
    [HttpDelete("user/delete-current-user-healthform")]
    public async Task<IResult> DeleteHealthForm(CancellationToken cancellationToken)
    {
        var command = new DeleteHealthFormCommand();
        var result = await _mediator.Send(command, cancellationToken);
        return result.MatchOk();
    }

    
    [Authorize(Roles = "Staff")]
    [HttpPut("user/update-user")]
    public async Task<IResult> UpdateByStaff( [FromBody] UpdateUserRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateUserCommand
        {
            UserId = request.UserId,
            FullName = request.FullName,
            Email = request.Email,
            Role = request.Role,
            Status = request.Status,
            BloodType = request.BloodType,
            IsDonor = request.IsDonor,
            DateOfBirth = request.DateOfBirth,
            Gender = request.Gender,
            Address = request.Address,
            Phone = request.Phone
        };

        var result = await _mediator.Send(command, cancellationToken);
        return result.MatchOk();
    }
    
    [Authorize]
    [HttpGet("user/get-current-users")]
    public async Task<IResult> GetCurrentUsers( CancellationToken cancellation)
    {
        var query = new GetCurrentUserQuery();
        var result = await _mediator.Send(query, cancellation);
        return result.MatchOk();
    }
    
    [HttpPost("user/create-donor-infor")]
    public async Task<IResult> CreateDonorInformation([FromBody] CreateDonorInformationRequest request, CancellationToken cancellationToken)
    {
        CreateDonorInformationCommand command = new CreateDonorInformationCommand
        {
           Weight = request.Weight,
           Height = request.Height,
           MedicalStatus = request.MedicalStatus,
        };
        
        Result<CreateDonorInformationResponse> result = await _mediator.Send(command, cancellationToken);
        return result.MatchCreated(id => $"/user/{id}");
    }
    
    // [Authorize]
    [HttpGet("user/get-donor-infor")]
    public async Task<IResult> GetDonorInformation([FromQuery] int pageNumber, [FromQuery] int pageSize, CancellationToken cancellation)
    {
        Result<Page<GetDonorInformationResponse>> result = await _mediator.Send(new GetDonorInformationQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
        }, cancellation);
        return result.MatchOk();
    }

    [Authorize]
    [HttpPut("user/update-donor-infor")]
    public async Task<IResult> UpdateDonorInformation([FromBody] UpdateDonorInformationRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateDonorInformationCommand
        {
            DonorInfoId = request.DonorInfoId,
            Weight = request.Weight,
            Height = request.Height,
            MedicalStatus = request.MedicalStatus,
        };
        
        var result = await _mediator.Send(command, cancellationToken);
        return result.MatchOk();
    }
}