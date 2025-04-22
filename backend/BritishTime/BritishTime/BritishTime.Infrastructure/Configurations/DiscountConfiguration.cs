using BritishTime.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BritishTime.Infrastructure.Configurations;

internal sealed class DiscountConfiguration : IEntityTypeConfiguration<Discount>
{
    public void Configure(EntityTypeBuilder<Discount> builder)
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
            .WithMany(b => b.Discounts)
            .HasForeignKey(c => c.BranchId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
