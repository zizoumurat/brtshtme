using BritishTime.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BritishTime.Infrastructure.Configurations;

internal sealed class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.Property(e => e.FirstName)
            .HasColumnType("nvarchar(40)")
            .IsRequired();

        builder.Property(e => e.LastName)
            .HasColumnType("nvarchar(40)")
            .IsRequired();

        builder.Property(e => e.IdentityNumber)
            .HasColumnType("nvarchar(11)")
            .IsRequired();

        builder.Property(e => e.Role)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(e => e.StartDate)
            .IsRequired();

        builder.Property(e => e.BirthDate)
            .IsRequired();

        builder.Property(e => e.Phone1)
            .HasColumnType("nvarchar(20)")
            .IsRequired();

        builder.Property(e => e.Phone2)
            .HasColumnType("nvarchar(20)");

        builder.Property(e => e.Phone3)
            .HasColumnType("nvarchar(20)");

        builder.Property(e => e.Email)
            .HasColumnType("nvarchar(100)");

        builder.Property(e => e.Address)
            .HasColumnType("nvarchar(250)");

        builder.Property(e => e.SalaryType)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(e => e.SalaryAmount)
            .HasColumnType("decimal(18,2)");

        builder.Property(e => e.ExtraPayment)
            .HasColumnType("decimal(18,2)");

        builder.Property(e => e.SalaryNote)
            .HasColumnType("nvarchar(500)");

        builder.Property(e => e.IsActive).IsRequired();

        builder.Property(e => e.BranchId).IsRequired();

        builder.HasOne(e => e.Branch)
            .WithMany(b => b.Employees)
            .HasForeignKey(e => e.BranchId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

