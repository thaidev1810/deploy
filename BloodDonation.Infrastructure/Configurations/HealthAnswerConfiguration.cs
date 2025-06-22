using BloodDonation.Domain.QuestionForm;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BloodDonation.Infrastructure.Configurations;

public class HealthAnswerConfiguration : IEntityTypeConfiguration<HealthAnswer>
{
    public void Configure(EntityTypeBuilder<HealthAnswer> builder)
    {
        builder.HasKey(x => x.AnswerId);

        builder.Property(x => x.Answer)
            .IsRequired();
    }
}