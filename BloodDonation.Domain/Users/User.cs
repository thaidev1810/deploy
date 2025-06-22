using BloodDonation.Domain.Donations;
using BloodDonation.Domain.QuestionForm;

namespace BloodDonation.Domain.Users;

public class User
{
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string? BloodType { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public UserGender Gender { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public UserRole Role { get; set; }
    public bool? IsDonor { get; set; }
    public DateTime? LastDonationDate { get; set; }
    public UserStatus Status { get; set; }
    public bool IsVerified { get; set; }
    
    // Các navigation property
    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    public ICollection<DonationRequest> DonationRequests { get; set; } = new List<DonationRequest>();

    public ICollection<DonationMatch> DonationMatches { get; set; } = new List<DonationMatch>();

    public ICollection<DonationHistory> DonationHistories { get; set; } = new List<DonationHistory>();

    public ICollection<DonationHistory> ConfirmedDonations { get; set; } = new List<DonationHistory>();

    public ICollection<BlogPost.BlogPost> BlogPosts { get; set; } = new List<BlogPost.BlogPost>();

    public DonorInformation? DonorInformation { get; set; }
    public ICollection<HealthForm> HealthForms { get; set; } = new List<HealthForm>();

}