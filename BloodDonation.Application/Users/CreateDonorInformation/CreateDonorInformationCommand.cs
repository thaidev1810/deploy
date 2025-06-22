using BloodDonation.Application.Abstraction.Messaging;
using BloodDonation.Domain.Users;

namespace BloodDonation.Application.Users.CreateDonorInformation;

public class CreateDonorInformationCommand : ICommand<CreateDonorInformationResponse>
{
    public decimal Weight { get; set; }
    public decimal Height { get; set; }
    public MedicalStatus MedicalStatus { get; set; }
    public DateTime LastChecked { get; set; }
}