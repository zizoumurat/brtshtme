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
    public DbSet<IncentiveSetting> IncentiveSettings => Set<IncentiveSetting>();
    public DbSet<LessonScheduleDefinition> LessonScheduleDefinitions => Set<LessonScheduleDefinition>();
    public DbSet<Region> Regions => Set<Region>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(DependencyInjection).Assembly);

        base.OnModelCreating(builder);
    }
}
