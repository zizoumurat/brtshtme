using BritishTime.Domain.Entities;
using BritishTime.Domain.Repositories.LessonSessionTemplates;
using BritishTime.Infrastructure.Context;

namespace BritishTime.Infrastructure.Repositories.LessonSessionTemplates;

public class CommandLessonSessionTemplateRepository : ICommandLessonSessionTemplateRepository
{
    private readonly ApplicationDbContext _context;

    public CommandLessonSessionTemplateRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(LessonSessionTemplate LessonSessionTemplate)
    {
        _context.LessonSessionTemplates.Add(LessonSessionTemplate);
        await _context.SaveChangesAsync();
    }

    public async Task AddRangeAsync(List<LessonSessionTemplate> LessonSessionTemplates)
    {
        await _context.LessonSessionTemplates.AddRangeAsync(LessonSessionTemplates);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(LessonSessionTemplate LessonSessionTemplate)
    {
        _context.LessonSessionTemplates.Update(LessonSessionTemplate);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(LessonSessionTemplate LessonSessionTemplate)
    {
        _context.LessonSessionTemplates.Remove(LessonSessionTemplate);
        await _context.SaveChangesAsync();
    }
}

