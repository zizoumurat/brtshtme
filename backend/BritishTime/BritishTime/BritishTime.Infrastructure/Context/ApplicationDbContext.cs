using BritishTime.Domain.Entities;
using GenericRepository;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BritishTime.Infrastructure.Context;
public sealed class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, Guid>, IUnitOfWork
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Branch> Branches => Set<Branch>();
    public DbSet<BranchPricingSetting> BranchPricingSettings => Set<BranchPricingSetting>();
    public DbSet<Campaign> Campaigns => Set<Campaign>();
    public DbSet<CourseSaleSetting> CourseSaleSettings => Set<CourseSaleSetting>();
    public DbSet<Discount> Discounts => Set<Discount>();
    public DbSet<IncentiveSetting> IncentiveSettings => Set<IncentiveSetting>();
    public DbSet<InstallmentSetting> InstallmentSettings => Set<InstallmentSetting>();
    public DbSet<LessonScheduleDefinition> LessonScheduleDefinitions => Set<LessonScheduleDefinition>();
    public DbSet<Region> Regions => Set<Region>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(DependencyInjection).Assembly);

        base.OnModelCreating(builder);
    }
}
