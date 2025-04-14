using BritishTime.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BritishTime.Infrastructure.Configurations;

internal sealed class IncentiveSettingConfiguration : IEntityTypeConfiguration<IncentiveSetting>
{
    public void Configure(EntityTypeBuilder<IncentiveSetting> builder)
    {
        builder.Property(b => b.MinAmount)
            .HasColumnType("decimal(18,2)");

        builder.Property(b => b.MaxAmount)
            .HasColumnType("decimal(18,2)");

        builder.Property(b => b.SalesCommission)
            .HasColumnType("decimal(5,2)");

        builder.Property(b => b.CollectionCommission)
            .HasColumnType("decimal(5,2)");

        builder.Property(b => b.Bonus)
            .HasColumnType("decimal(18,2)");

        builder.Property(b => b.Note)
            .HasColumnType("nvarchar(max)");

        builder.Property(b => b.ParticipantType)
            .HasConversion<int>(); 
    }
}

