using BritishTime.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BritishTime.Infrastructure.Configurations;
internal sealed class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.Property(p => p.FirstName).HasColumnType("nvarchar(50)");
        builder.Property(p => p.LastName).HasColumnType("nvarchar(50)");

        builder.HasOne(x => x.Branch)
           .WithMany(b => b.Users)
           .HasForeignKey(x => x.BranchId);
    }
}
