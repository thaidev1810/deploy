using BloodDonation.Application.Abstraction.Authentication;
using BloodDonation.Application.Abstraction.Data;
using BloodDonation.Application.Abstraction.Messaging;
using BloodDonation.Domain.Common;
using BloodDonation.Domain.Users;
using BloodDonation.Domain.Users.Errors;
using Microsoft.EntityFrameworkCore;

namespace BloodDonation.Application.Users.Login;

public class LoginWithRefreshToken (IDbContext context, 
    ITokenProvider tokenProvider) 
    : ICommandHandler<LoginWithRefreshToken.LoginByRefreshTokenCommand , TokenResponse>
{
    public async Task<Result<TokenResponse>> Handle(LoginByRefreshTokenCommand request,
        CancellationToken cancellationToken)
    {
        RefreshToken? refreshToken = await context.RefreshTokens
            .Include(r => r.User)
            .FirstOrDefaultAsync(t => t.Token == request.RefreshToken, cancellationToken);

        if (refreshToken == null || refreshToken.Expires < DateTime.UtcNow)
        {
            return Result.Failure<TokenResponse>(UserErrors.InvalidRefreshToken);

        }

        string accessToken = tokenProvider.Create(refreshToken.User);
        refreshToken.Token = tokenProvider.GenerateRefreshToken();
        refreshToken.Expires = DateTime.UtcNow.AddDays(1);
        
        await context.SaveChangesAsync(cancellationToken);
        return new TokenResponse()
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken.Token
        };
    }

    public sealed class LoginByRefreshTokenCommand : ICommand<TokenResponse>
    {
        public string RefreshToken { get; set; }
    }
}