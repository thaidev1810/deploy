using BloodDonation.Application.Abstraction.Messaging;
using BloodDonation.Domain.Bloods;

namespace BloodDonation.Application.Bloods.CheckBloodCompatibility;

public class CheckBloodCompatibilityQuery : IQuery<CheckBloodCompatibilityResponse>
{
    public string FromBloodType { get; set; }
    public string ToBloodType { get; set; }
    public BloodComponentType ComponentType { get; set; }
}