using BritishTime.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BritishTime.Infrastructure.Configurations;

internal class LessonSessionTemplateConfiguration : IEntityTypeConfiguration<LessonSessionTemplate>
{
    public void Configure(EntityTypeBuilder<LessonSessionTemplate> builder)
    {
        builder.ToTable("LessonSessionTemplates");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Day)
            .IsRequired();

        builder.HasOne(x => x.CourseClass)
            .WithMany()
            .HasForeignKey(x => x.CourseClassId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Teacher)
            .WithMany()
            .HasForeignKey(x => x.TeacherId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new { x.CourseClassId, x.Day })
            .IsUnique(); // Her gün için bir öğretmen atanmış olmalı
    }
}
