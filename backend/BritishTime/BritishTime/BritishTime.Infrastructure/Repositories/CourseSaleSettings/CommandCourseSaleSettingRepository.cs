using BritishTime.Domain.Entities;
using BritishTime.Domain.Repositories.CourseSaleSettings;
using BritishTime.Infrastructure.Context;

namespace BritishTime.Infrastructure.Repositories.CourseSaleSettings;

public class CommandCourseSaleSettingRepository : ICommandCourseSaleSettingRepository
{
    private readonly ApplicationDbContext _context;

    public CommandCourseSaleSettingRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(CourseSaleSetting CourseSaleSetting)
    {
        _context.CourseSaleSettings.Add(CourseSaleSetting);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(CourseSaleSetting CourseSaleSetting)
    {
        _context.CourseSaleSettings.Update(CourseSaleSetting);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(CourseSaleSetting CourseSaleSetting)
    {
        _context.CourseSaleSettings.Remove(CourseSaleSetting);
        await _context.SaveChangesAsync();
    }
}

