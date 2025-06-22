using BloodDonation.Apis.Extensions;
using BloodDonation.Apis.Requests;
using BloodDonation.Application.Users.Login;
using BloodDonation.Application.Users.Register;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonation.Apis.Controller;


[Route("api/")]
[ApiController]
public class AuthenticateController : ControllerBase
{
    private readonly ISender _mediator;

    public AuthenticateController(ISender mediator)
    {
        _mediator = mediator;
    }
    
    
    [HttpPost("auth/login")]
    public async Task<IResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        LoginCommand command = new LoginCommand
        {
            Email = request.Email,
            Password = request.Password
        };
        
        var  result = await _mediator.Send(command, cancellationToken);
        return result.MatchOk();
    }
    
    [HttpPost("auth/loginWithRefreshToken")]
    public async Task<IResult> LoginWithRefreshToken([FromBody] string refreshToken, CancellationToken cancellationToken)
    {
        LoginWithRefreshToken.LoginByRefreshTokenCommand command = new LoginWithRefreshToken.LoginByRefreshTokenCommand
        {
            RefreshToken = refreshToken
        };
        
        var  result = await _mediator.Send(command, cancellationToken);
        return result.MatchOk();
    }
    
    [HttpPost("auth/register")]
    public async Task<IResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
    {
        RegisterCommand command = new RegisterCommand
        {
            Name = request.Name,
            Email = request.Email,
            Password = request.Password,
            Phone = request.Phone,
            Address = request.Address,
            DateOfBirth = request.DateOfBirth,
            Gender = request.Gender
        };
        
        var result = await _mediator.Send(command, cancellationToken);
        return result.MatchOk();
    }
    
}