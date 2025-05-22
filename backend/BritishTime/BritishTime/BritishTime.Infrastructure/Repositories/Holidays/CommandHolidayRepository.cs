using BritishTime.Domain.Entities;
using BritishTime.Domain.Repositories.Holidays;
using BritishTime.Infrastructure.Context;

namespace BritishTime.Infrastructure.Repositories.Holidays;

public class CommandHolidayRepository : ICommandHolidayRepository
{
    private readonly ApplicationDbContext _context;

    public CommandHolidayRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Holiday Holiday)
    {
        _context.Holidays.Add(Holiday);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Holiday Holiday)
    {
        _context.Holidays.Update(Holiday);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Holiday Holiday)
    {
        _context.Holidays.Remove(Holiday);
        await _context.SaveChangesAsync();
    }
}

