using BritishTime.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BritishTime.Infrastructure.Configurations;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.Property(p => p.Amount).HasColumnType("decimal(18,2)");
        builder.Property(p => p.Description).HasMaxLength(500);

        builder.HasOne(p => p.Student)
               .WithMany(s => s.Payments)
               .HasForeignKey(p => p.StudentId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.Installment)
               .WithMany(i => i.Payments)
               .HasForeignKey(p => p.InstallmentId)
               .OnDelete(DeleteBehavior.SetNull);
    }
}
