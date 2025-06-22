using BloodDonation.Domain.Common;

namespace BloodDonation.Domain.Donations.Errors;

public class DonationMatchErrors
{
    public static readonly Error MatchNotFound = Error.NotFound(
        "Match.NotFound",
        "Match not found");
    public static readonly Error NotDonor = Error.Problem(
        "NotDonorToConfirm",
        "You are not authorized to confirm this match");
}