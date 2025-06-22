using BloodDonation.Domain.Users;

namespace BloodDonation.Application.Abstraction.Authentication;

public interface ITokenProvider
{
    string Create(User user);
    string GenerateRefreshToken();
}
