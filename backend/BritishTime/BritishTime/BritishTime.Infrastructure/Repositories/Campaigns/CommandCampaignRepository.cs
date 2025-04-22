using BritishTime.Domain.Entities;
using BritishTime.Domain.Repositories.Campaigns;
using BritishTime.Infrastructure.Context;

namespace BritishTime.Infrastructure.Repositories.Campaigns;

public class CommandCampaignRepository : ICommandCampaignRepository
{
    private readonly ApplicationDbContext _context;

    public CommandCampaignRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Campaign Campaign)
    {
        _context.Campaigns.Add(Campaign);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Campaign Campaign)
    {
        _context.Campaigns.Update(Campaign);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Campaign Campaign)
    {
        _context.Campaigns.Remove(Campaign);
        await _context.SaveChangesAsync();
    }
}

