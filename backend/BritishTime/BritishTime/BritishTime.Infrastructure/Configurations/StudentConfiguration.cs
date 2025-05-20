using BritishTime.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BritishTime.Infrastructure.Configurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.Property(s => s.FirstName).IsRequired().HasMaxLength(100);
        builder.Property(s => s.LastName).IsRequired().HasMaxLength(100);
        builder.Property(s => s.IdentityNumber).HasMaxLength(11);
        builder.Property(s => s.Phone).HasMaxLength(20);
        builder.Property(s => s.SecondPhone).HasMaxLength(20);
        builder.Property(s => s.Email).HasMaxLength(100);
        builder.Property(s => s.Address).HasMaxLength(500);

        builder.Property(s => s.ParentFirstName).HasMaxLength(100);
        builder.Property(s => s.ParentLastName).HasMaxLength(100);
        builder.Property(s => s.ParentPhone).HasMaxLength(20);
        builder.Property(s => s.ParentIdentityNumber).HasMaxLength(11);

        builder.HasOne(s => s.Branch)
               .WithMany()
               .HasForeignKey(s => s.BranchId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(s => s.CrmRecord)
               .WithOne()
               .HasForeignKey<Student>(s => s.CrmRecordId)
               .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(s => s.User)
               .WithMany()
               .HasForeignKey(s => s.UserId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
