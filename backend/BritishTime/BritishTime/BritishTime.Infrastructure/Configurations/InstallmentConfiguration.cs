using BritishTime.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BritishTime.Infrastructure.Configurations;

public class InstallmentConfiguration : IEntityTypeConfiguration<Installment>
{
    public void Configure(EntityTypeBuilder<Installment> builder)
    {
        builder.Property(i => i.Amount).HasColumnType("decimal(18,2)");

        builder.HasOne(i => i.Contract)
               .WithMany(c => c.Installments)
               .HasForeignKey(i => i.ContractId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
