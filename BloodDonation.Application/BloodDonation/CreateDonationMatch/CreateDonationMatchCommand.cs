using BloodDonation.Application.Abstraction.Messaging;
using BloodDonation.Domain.Bloods;
using BloodDonation.Domain.Donations;

namespace BloodDonation.Application.BloodDonation.CreateDonationMatch;

public class CreateDonationMatchCommand : ICommand<CreateDonationMatchResponse>
{
    public Guid RequestId { get; set; }
    public Guid DonorId { get; set; }
}