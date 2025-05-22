using BritishTime.Domain.Dtos;
using BritishTime.Domain.Entities;
using System.Linq.Expressions;

namespace BritishTime.Domain.Repositories.LessonSessions;

public interface IQueryLessonSessionRepository
{
    Task<bool> IsExistingAsync(Expression<Func<LessonSession, bool>> predicate);
    IQueryable<LessonSession> GetList(Expression<Func<LessonSession, bool>> expression, bool isTracking = false);
    Task<LessonSessionDto> GetByIdAsync(Guid id);
}
