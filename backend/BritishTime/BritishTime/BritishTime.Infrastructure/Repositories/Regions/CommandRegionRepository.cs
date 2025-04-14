using BritishTime.Domain.Entities;
using BritishTime.Domain.Repositories.Regions;
using BritishTime.Infrastructure.Context;

namespace BritishTime.Infrastructure.Repositories.Regions;

public class CommandRegionRepository : ICommandRegionRepository
{
    private readonly ApplicationDbContext _context;

    public CommandRegionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Region Region)
    {
        _context.Regions.Add(Region);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Region Region)
    {
        _context.Regions.Update(Region);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Region Region)
    {
        _context.Regions.Remove(Region);
        await _context.SaveChangesAsync();
    }
}

