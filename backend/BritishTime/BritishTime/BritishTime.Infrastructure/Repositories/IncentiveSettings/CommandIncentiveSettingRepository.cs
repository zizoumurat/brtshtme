using BritishTime.Domain.Entities;
using BritishTime.Domain.Repositories.IncentiveSettings;
using BritishTime.Infrastructure.Context;

namespace BritishTime.Infrastructure.Repositories.IncentiveSettings;

public class CommandIncentiveSettingRepository : ICommandIncentiveSettingRepository
{
    private readonly ApplicationDbContext _context;

    public CommandIncentiveSettingRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(IncentiveSetting IncentiveSetting)
    {
        _context.IncentiveSettings.Add(IncentiveSetting);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(IncentiveSetting IncentiveSetting)
    {
        _context.IncentiveSettings.Update(IncentiveSetting);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(IncentiveSetting IncentiveSetting)
    {
        _context.IncentiveSettings.Remove(IncentiveSetting);
        await _context.SaveChangesAsync();
    }
}

