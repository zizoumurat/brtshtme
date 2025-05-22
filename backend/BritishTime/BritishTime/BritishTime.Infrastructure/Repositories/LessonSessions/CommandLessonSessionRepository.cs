using BritishTime.Domain.Entities;
using BritishTime.Domain.Repositories.LessonSessions;
using BritishTime.Infrastructure.Context;

namespace BritishTime.Infrastructure.Repositories.LessonSessions;

public class CommandLessonSessionRepository : ICommandLessonSessionRepository
{
    private readonly ApplicationDbContext _context;

    public CommandLessonSessionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(LessonSession LessonSession)
    {
        _context.LessonSessions.Add(LessonSession);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(LessonSession LessonSession)
    {
        _context.LessonSessions.Update(LessonSession);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(LessonSession LessonSession)
    {
        _context.LessonSessions.Remove(LessonSession);
        await _context.SaveChangesAsync();
    }

    public async Task AddRangeAsync(List<LessonSession> LessonSessions)
    {
        await _context.LessonSessions.AddRangeAsync(LessonSessions);
        await _context.SaveChangesAsync();
    }
}

