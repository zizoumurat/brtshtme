using BritishTime.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BritishTime.Infrastructure.Configurations;

internal sealed class CrmRecordConfiguration : IEntityTypeConfiguration<CrmRecord>
{
    public void Configure(EntityTypeBuilder<CrmRecord> builder)
    {
        builder.Property(x => x.Phone)
            .HasColumnType("nvarchar(20)")
            .IsRequired();

        builder.Property(x => x.FirstName)
            .HasColumnType("nvarchar(100)")
            .IsRequired();

        builder.Property(x => x.LastName)
            .HasColumnType("nvarchar(100)")
            .IsRequired();

        builder.Property(x => x.SecondPhone)
            .HasColumnType("nvarchar(20)");

        builder.Property(x => x.Email)
            .HasColumnType("nvarchar(100)");

        builder.Property(x => x.Description)
            .HasColumnType("nvarchar(500)");

        builder.Property(x => x.Date)
            .IsRequired();

        builder.Property(x => x.SalesRepresentativeId)
            .IsRequired();

        builder.Property(x => x.ExcludeFromCommission)
            .IsRequired();

        builder.Property(x => x.DataSource)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.Status)
            .HasConversion<int>()
            .IsRequired();

        builder.HasOne(x => x.DataProvider)
            .WithMany()
            .HasForeignKey(x => x.DataProviderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.SalesRepresentative)
            .WithMany()
            .HasForeignKey(x => x.SalesRepresentativeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Region)
            .WithMany()
            .HasForeignKey(x => x.RegionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Branch)
           .WithMany()
           .HasForeignKey(x => x.BranchId)
           .OnDelete(DeleteBehavior.Restrict);
    }
}
