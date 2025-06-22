

using BloodDonation.Domain.Users;

namespace BloodDonation.Application.Users.GetCurrentUser
{
    public sealed record GetCurrentUserResponse
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string? BloodType { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public UserGender Gender { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public bool? IsDonor { get; set; }
        public DateTime? LastDonationDate { get; set; }
        public UserStatus Status { get; set; }
        public DonorInformation? DonorInformation { get; set; }

    }
    public class DonorInformation
    {
        public decimal Weight { get; set; }
        public decimal Height { get; set; }
        public string? MedicalStatus { get; set; } 
        public DateTime LastChecked { get; set; }
    }
}
