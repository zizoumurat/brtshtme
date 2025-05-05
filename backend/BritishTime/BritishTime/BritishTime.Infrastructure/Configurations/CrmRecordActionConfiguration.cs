using BritishTime.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BritishTime.Infrastructure.Configurations;

public class CrmRecordActionConfiguration : IEntityTypeConfiguration<CrmRecordAction>
{
    public void Configure(EntityTypeBuilder<CrmRecordAction> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Description)
               .HasMaxLength(1000);

        builder.Property(x => x.Date)
               .IsRequired();

        builder.Property(x => x.ActionType)
               .IsRequired();

        builder.HasOne(x => x.CrmRecord)
               .WithMany(x => x.Actions)
               .HasForeignKey(x => x.CrmRecordId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Employee)
               .WithMany()
               .HasForeignKey(x => x.EmployeeId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
