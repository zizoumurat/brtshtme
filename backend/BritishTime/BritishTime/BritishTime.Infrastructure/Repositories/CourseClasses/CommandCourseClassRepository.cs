using BritishTime.Domain.Entities;
using BritishTime.Domain.Repositories.CourseClasses;
using BritishTime.Infrastructure.Context;

namespace BritishTime.Infrastructure.Repositories.CourseClasses;

public class CommandCourseClassRepository : ICommandCourseClassRepository
{
    private readonly ApplicationDbContext _context;

    public CommandCourseClassRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(CourseClass CourseClass)
    {
        _context.CourseClasses.Add(CourseClass);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(CourseClass CourseClass)
    {
        _context.CourseClasses.Update(CourseClass);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(CourseClass CourseClass)
    {
        _context.CourseClasses.Remove(CourseClass);
        await _context.SaveChangesAsync();
    }
}

