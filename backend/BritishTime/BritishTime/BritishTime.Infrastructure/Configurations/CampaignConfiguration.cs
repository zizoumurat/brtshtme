using BritishTime.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BritishTime.Infrastructure.Configurations;

internal sealed class CampaignConfiguration : IEntityTypeConfiguration<Campaign>
{
    public void Configure(EntityTypeBuilder<Campaign> builder)
    {
        builder.Property(c => c.Definition)
            .HasColumnType("nvarchar(250)")
            .IsRequired();

        builder.Property(c => c.DiscountRate)
            .HasColumnType("decimal(5,3)")
            .IsRequired();

        builder.Property(c => c.IsActive)
            .IsRequired();

        builder.Property(c => c.BranchId)
            .IsRequired();

        builder.HasOne(c => c.Branch)
            .WithMany(b => b.Campaigns)
            .HasForeignKey(c => c.BranchId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
