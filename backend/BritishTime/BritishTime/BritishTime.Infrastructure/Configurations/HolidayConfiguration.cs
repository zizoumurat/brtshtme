using BritishTime.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BritishTime.Infrastructure.Configurations;

public class HolidayConfiguration : IEntityTypeConfiguration<Holiday>
{
    public void Configure(EntityTypeBuilder<Holiday> builder)
    {
        builder.ToTable("Holidays");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Date)
               .IsRequired();

        builder.Property(x => x.Description)
               .IsRequired()
               .HasMaxLength(250);

        builder.HasIndex(x => new { x.Date, x.BranchId, x.CourseClassId })
               .IsUnique(); // Aynı gün aynı sınıfa/şubeye ikinci tatil girilmesin
    }
}
