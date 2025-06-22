namespace BloodDonation.Application.Abstraction.Authentication;

public interface IUserContext
{
    Guid UserId { get; }
}