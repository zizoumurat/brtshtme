using BritishTime.Domain.Entities;
using GenericRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BritishTime.Infrastructure.Context;
public sealed class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, Guid,
    IdentityUserClaim<Guid>, AppUserRole, IdentityUserLogin<Guid>,
    IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>, IUnitOfWork
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Branch> Branches => Set<Branch>();
    public DbSet<BranchPricingSetting> BranchPricingSettings => Set<BranchPricingSetting>();
    public DbSet<Campaign> Campaigns => Set<Campaign>();
    public DbSet<ClassRoom> ClassRooms => Set<ClassRoom>();
    public DbSet<CrmRecord> CrmRecords => Set<CrmRecord>();
    public DbSet<CrmRecordAction> CrmRecordActions => Set<CrmRecordAction>();
    public DbSet<CourseSaleSetting> CourseSaleSettings => Set<CourseSaleSetting>();
    public DbSet<Discount> Discounts => Set<Discount>();
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Level> Levels => Set<Level>();
    public DbSet<IncentiveSetting> IncentiveSettings => Set<IncentiveSetting>();
    public DbSet<InstallmentSetting> InstallmentSettings => Set<InstallmentSetting>();
    public DbSet<LessonScheduleDefinition> LessonScheduleDefinitions => Set<LessonScheduleDefinition>();
    public DbSet<Region> Regions => Set<Region>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(DependencyInjection).Assembly);

        base.OnModelCreating(builder);

        builder.Entity<AppUserRole>(userRole =>
        {
            userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

            userRole.HasOne(ur => ur.User)
                    .WithMany(u => u.AppUserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();

            userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.AppUserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();
        });
    }
}
