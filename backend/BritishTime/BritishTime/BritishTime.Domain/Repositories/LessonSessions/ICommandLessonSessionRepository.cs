using BritishTime.Domain.Entities;

namespace BritishTime.Domain.Repositories.LessonSessions;

public interface ICommandLessonSessionRepository
{
    Task AddAsync(LessonSession LessonSession);
    Task AddRangeAsync(List<LessonSession> LessonSessions);
    Task UpdateAsync(LessonSession LessonSession);
    Task DeleteAsync(LessonSession LessonSession);
}