using BritishTime.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BritishTime.Infrastructure.Configurations;

public class ContractConfiguration : IEntityTypeConfiguration<Contract>
{
    public void Configure(EntityTypeBuilder<Contract> builder)
    {
        builder.Property(c => c.TotalAmount).HasColumnType("decimal(18,2)");
        builder.Property(c => c.Deposit).HasColumnType("decimal(18,2)");

        builder.HasOne(c => c.Student)
               .WithMany(s => s.Contracts)
               .HasForeignKey(c => c.StudentId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(c => c.LessonScheduleDefinition)
               .WithMany()
               .HasForeignKey(c => c.LessonScheduleId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.Campaign)
               .WithMany()
               .HasForeignKey(c => c.CampaignId)
               .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(c => c.Discount)
               .WithMany()
               .HasForeignKey(c => c.DiscountId)
               .OnDelete(DeleteBehavior.SetNull);
    }
}
