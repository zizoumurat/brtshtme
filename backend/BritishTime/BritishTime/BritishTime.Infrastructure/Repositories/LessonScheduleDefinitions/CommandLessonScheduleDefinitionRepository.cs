using BritishTime.Domain.Entities;
using BritishTime.Domain.Repositories.LessonScheduleDefinitions;
using BritishTime.Infrastructure.Context;

namespace BritishTime.Infrastructure.Repositories.LessonScheduleDefinitions;

public class CommandLessonScheduleDefinitionRepository : ICommandLessonScheduleDefinitionRepository
{
    private readonly ApplicationDbContext _context;

    public CommandLessonScheduleDefinitionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(LessonScheduleDefinition LessonScheduleDefinition)
    {
        _context.LessonScheduleDefinitions.Add(LessonScheduleDefinition);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(LessonScheduleDefinition LessonScheduleDefinition)
    {
        _context.LessonScheduleDefinitions.Update(LessonScheduleDefinition);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(LessonScheduleDefinition LessonScheduleDefinition)
    {
        _context.LessonScheduleDefinitions.Remove(LessonScheduleDefinition);
        await _context.SaveChangesAsync();
    }
}

