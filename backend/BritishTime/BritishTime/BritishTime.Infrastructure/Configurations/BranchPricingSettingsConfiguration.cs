using BritishTime.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BritishTime.Infrastructure.Configurations;

internal sealed class BranchPricingSettingsConfiguration : IEntityTypeConfiguration<BranchPricingSetting>
{
    public void Configure(EntityTypeBuilder<BranchPricingSetting> builder)
    {
        builder.ToTable("BranchPricingSettings");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.HourlyRate)
            .HasColumnType("decimal(18,3)");

        builder.Property(b => b.DiscountForPrepayment)
            .HasColumnType("decimal(5,3)");

        builder.Property(b => b.CashPrepaymentDiscount)
            .HasColumnType("decimal(5,3)");

        builder.Property(b => b.CreditCardInstallmentDiscount)
            .HasColumnType("decimal(5,3)");

        builder.Property(b => b.InstallmentRate)
            .HasColumnType("decimal(8,5)");

        builder.Property(b => b.CollectionRateForBonus)
            .HasColumnType("decimal(5,3)");

        builder.HasOne(b => b.Branch)
            .WithMany(b => b.BranchPricingSettings)
            .HasForeignKey(b => b.BranchId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
