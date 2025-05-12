using BritishTime.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BritishTime.Infrastructure.Configurations;

internal sealed class LevelConfiguration : IEntityTypeConfiguration<Level>
{
    public void Configure(EntityTypeBuilder<Level> builder)
    {
        builder.Property(c => c.Name)
            .HasColumnType("nvarchar(40)")
            .IsRequired();

        builder.Property(c => c.Definition).HasColumnType("nvarchar(40)");
    }
}
