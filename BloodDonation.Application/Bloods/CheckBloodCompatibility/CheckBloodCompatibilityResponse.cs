using BloodDonation.Domain.Bloods;

namespace BloodDonation.Application.Bloods.CheckBloodCompatibility;

public class CheckBloodCompatibilityResponse
{
    public string FromBloodType { get; set; } = default!;
    public string ToBloodType { get; set; } = default!;
    public BloodComponentType ComponentType { get; set; }
    public bool IsCompatible { get; set; }
}