using BloodDonation.Application.Abstraction.Data;
using BloodDonation.Domain.BlogPost;
using BloodDonation.Domain.Bloods;
using BloodDonation.Domain.Common;
using BloodDonation.Domain.Donations;
using BloodDonation.Domain.QuestionForm;
using BloodDonation.Domain.Users;
using BloodDonation.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BloodDonation.Infrastructure.Database;

public sealed class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options,
    IPublisher publisher)
    : DbContext(options), IDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<DonorInformation> DonorInformation { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<BloodType> BloodTypes { get; set; }
    public DbSet<BloodStored> BloodStored { get; set; }
    public DbSet<BloodCompatibility> BloodCompatibility { get; set; }
    public DbSet<DonationRequest> DonationRequests { get; set; }
    public DbSet<DonationMatch> DonationMatches { get; set; }
    public DbSet<DonationHistory> DonationsHistory { get; set; }
    public DbSet<BlogPost> BlogPosts { get; set; }
    public DbSet<HealthForm> HealthForms { get; set; }
    public DbSet<HealthQuestion> HealthQuestions { get; set; }
    public DbSet<HealthAnswer> HealthAnswers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        modelBuilder.HasDefaultSchema(Schemas.Default);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        int result = await base.SaveChangesAsync(cancellationToken);

        await PublishDomainEventsAsync();

        return result;
    }

    private async Task PublishDomainEventsAsync()
    {
        var domainEvents = ChangeTracker
            .Entries<Entity>()
            .Select(entry => entry.Entity)
            .SelectMany(entity =>
            {
                var events = entity.DomainEvents;
                entity.ClearDomainEvents();
                return events;
            })
            .ToList();

        foreach (var domainEvent in domainEvents)
        {
            await publisher.Publish(domainEvent);
        }
    }
}