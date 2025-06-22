using BloodDonation.Application.Abstraction.Messaging;

namespace BloodDonation.Application.Users.Login;

public sealed class LoginCommand() : ICommand<TokenResponse>
{
    public string Email { get; init; }
    public string Password { get; init; }
}