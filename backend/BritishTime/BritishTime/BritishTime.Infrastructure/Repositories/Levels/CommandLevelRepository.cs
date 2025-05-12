using BritishTime.Domain.Entities;
using BritishTime.Domain.Repositories.Levels;
using BritishTime.Infrastructure.Context;

namespace BritishTime.Infrastructure.Repositories.Levels;

public class CommandLevelRepository : ICommandLevelRepository
{
    private readonly ApplicationDbContext _context;

    public CommandLevelRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Level Level)
    {
        _context.Levels.Add(Level);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Level Level)
    {
        _context.Levels.Update(Level);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Level Level)
    {
        _context.Levels.Remove(Level);
        await _context.SaveChangesAsync();
    }
}

