using BloodDonation.Application.Abstraction.Messaging;
using BloodDonation.Domain.Common;

namespace BloodDonation.Application.BloodDonation.ConfirmDonationMatch;

public class ConfirmDonationMatchCommand : ICommand
{
    public Guid MatchId { get; set; }
    public bool IsAccepted { get; set; }
}