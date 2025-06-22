using BloodDonation.Domain.QuestionForm;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BloodDonation.Infrastructure.Configurations;

public class HealthFormConfiguration : IEntityTypeConfiguration<HealthForm>
{
    public void Configure(EntityTypeBuilder<HealthForm> builder)
    {
        builder.HasKey(x => x.FormId);

        builder.Property(x => x.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(x => x.Status)
            .HasConversion<string>() 
            .HasDefaultValue(FormStatus.Pending)
            .IsRequired();

        builder.Property(x => x.ApprovedBy)
            .IsRequired(false);

        builder.Property(x => x.ApprovedAt)
            .IsRequired(false);

        builder.Property(x => x.ApprovedByStaffName)
            .HasMaxLength(255)
            .IsRequired(false);

        builder.HasOne(x => x.User)
            .WithMany(u => u.HealthForms)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.ApprovedByStaff)
            .WithMany()
            .HasForeignKey(x => x.ApprovedBy)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Answers)
            .WithOne(x => x.Form)
            .HasForeignKey(x => x.FormId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}