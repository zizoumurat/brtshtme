using BritishTime.Domain.Entities;
using BritishTime.Domain.Repositories.BranchPricingSettings;
using BritishTime.Infrastructure.Context;

namespace BritishTime.Infrastructure.Repositories.BranchPricingSettings;

public class CommandBranchPricingSettingRepository : ICommandBranchPricingSettingRepository
{
    private readonly ApplicationDbContext _context;

    public CommandBranchPricingSettingRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(BranchPricingSetting BranchPricingSetting)
    {
        _context.BranchPricingSettings.Add(BranchPricingSetting);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(BranchPricingSetting BranchPricingSetting)
    {
        _context.BranchPricingSettings.Update(BranchPricingSetting);
        await _context.SaveChangesAsync();
    }
}

