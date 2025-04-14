using BritishTime.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BritishTime.Infrastructure.Configurations;

internal sealed class BranchConfiguration : IEntityTypeConfiguration<Branch>
{
    public void Configure(EntityTypeBuilder<Branch> builder)
    {
        builder.Property(b => b.Name).HasColumnType("nvarchar(40)").IsRequired();
        builder.Property(b => b.PhoneNumber).IsRequired();
        builder.Property(b => b.Email).IsRequired();
        builder.Property(b => b.LessonDurationInMinutes).IsRequired();
        builder.Property(b => b.BreakDurationInMinutes).IsRequired();
        builder.Property(b => b.LevelDurationInHours).IsRequired();

        builder.HasData(
            new Branch
            {
                Id = Guid.Parse("c3740f2d-2ef8-4d45-a7df-d1a5bb1577c6"),
                Name = "Merkez",
                Email = "info@britishtime.com.tr",
                PhoneNumber = "+90 212 123 45 67"
            }
        );
    }
}
