using BritishTime.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BritishTime.Infrastructure.Configurations;

internal sealed class LessonScheduleDefinitionConfiguration : IEntityTypeConfiguration<LessonScheduleDefinition>
{
    public void Configure(EntityTypeBuilder<LessonScheduleDefinition> builder)
    {
        builder.Property(x => x.Schedule).HasColumnType("nvarchar(20)").IsRequired();
        builder.Property(x => x.ScheduleCode).HasColumnType("nvarchar(120)").IsRequired();

        builder.Property(x => x.DayCount).IsRequired();
        builder.Property(x => x.DayHour).IsRequired();
        builder.Property(x => x.Days)
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(x => Enum.Parse<DayOfWeek>(x)).ToList()
                )
                .Metadata.SetValueComparer(
                    new ValueComparer<List<DayOfWeek>>(
                        (c1, c2) => c1.SequenceEqual(c2),
                        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                        c => c.ToList()
                    )
                );
        builder.Property(x => x.StartTime).IsRequired();
        builder.Property(x => x.EndTime).IsRequired();

        builder.Property(x => x.StudentType).HasConversion<string>().IsRequired();
        builder.Property(x => x.EducationType).HasConversion<string>().IsRequired();
        builder.Property(x => x.ScheduleCategory).HasConversion<string>().IsRequired();
        builder.Property(x => x.Discount).HasPrecision(5, 2);

        builder.HasOne(x => x.Branch)
               .WithMany(b => b.LessonScheduleDefinitions)
               .HasForeignKey(x => x.BranchId);
    }
}
