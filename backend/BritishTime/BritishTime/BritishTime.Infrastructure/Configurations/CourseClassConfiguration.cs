using BritishTime.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BritishTime.Infrastructure.Configurations;
internal sealed class CourseClassConfiguration : IEntityTypeConfiguration<CourseClass>
{
    public void Configure(EntityTypeBuilder<CourseClass> builder)
    {
        builder.ToTable("CourseClasses");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.BranchId)
            .IsRequired();

        builder.Property(c => c.Name)
            .HasMaxLength(80);

        builder.Property(c => c.Note)
                .HasMaxLength(80);

        builder.Property(c => c.ClassType)
            .IsRequired();

        builder.Property(c => c.EducationType)
            .IsRequired();

        builder.Property(c => c.ScheduleType)
            .IsRequired();

        builder.Property(c => c.LessonScheduleDefinitionId)
            .IsRequired();

        builder.Property(c => c.LevelId)
            .IsRequired();

        builder.Property(c => c.Description)
            .HasMaxLength(400);

        builder.Property(c => c.StartDate)
            .IsRequired();

        builder.Property(c => c.EndDate)
            .IsRequired();

        builder.Property(c => c.Capacity)
            .IsRequired();

        builder.Property(c => c.Unit)
            .IsRequired();

        builder.Property(c => c.ClassroomId)
            .IsRequired(false);

        builder.Property(c => c.CreatedAt)
            .IsRequired();

        // İlişkiler (isteğe bağlı olarak eklenebilir)
        builder.HasOne(e => e.Branch)
            .WithMany(b => b.CourseClasses)
            .HasForeignKey(e => e.BranchId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Level>()
            .WithMany()
            .HasForeignKey(c => c.LevelId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<LessonScheduleDefinition>()
            .WithMany()
            .HasForeignKey(c => c.LessonScheduleDefinitionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e=> e.ClassRoom)
            .WithMany()
            .HasForeignKey(c => c.ClassroomId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
