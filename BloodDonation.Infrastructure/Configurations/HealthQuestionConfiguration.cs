using BloodDonation.Domain.QuestionForm;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BloodDonation.Infrastructure.Configurations;

public class HealthQuestionConfiguration : IEntityTypeConfiguration<HealthQuestion>
{
    public void Configure(EntityTypeBuilder<HealthQuestion> builder)
    {
        builder.HasKey(x => x.QuestionId);

        builder.Property(x => x.Content)
            .IsRequired();

        builder.Property(x => x.IsRequired)
            .IsRequired();

        builder.Property(x => x.QuestionType)
            .HasConversion<string>() 
            .IsRequired();

        builder.HasMany(x => x.Answers)
            .WithOne(x => x.Question)
            .HasForeignKey(x => x.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}