using BritishTime.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BritishTime.Infrastructure.Configurations;

internal sealed class CourseSaleSettingConfiguration : IEntityTypeConfiguration<CourseSaleSetting>
{
    public void Configure(EntityTypeBuilder<CourseSaleSetting> builder)
    {
        builder.Property(c => c.MinLevel).IsRequired();

        builder.Property(c => c.MaxLevel).IsRequired();

        builder.Property(c => c.Rate).HasColumnType("decimal(5,3)").IsRequired();

        builder.HasOne(c => c.Branch)
            .WithMany(b => b.CourseSaleSettings)
            .HasForeignKey(c => c.BranchId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

internal sealed class InstallmentSettingConfiguration : IEntityTypeConfiguration<InstallmentSetting>
{
    public void Configure(EntityTypeBuilder<InstallmentSetting> builder)
    {
        builder.Property(c => c.Level).IsRequired();

        builder.Property(c => c.MaxBond).IsRequired();
        builder.Property(c => c.MaxCardInstallment).IsRequired();

        builder.Property(c => c.BranchId).IsRequired();

        builder.HasOne(c => c.Branch)
            .WithMany(b => b.InstallmentSettings)
            .HasForeignKey(c => c.BranchId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}