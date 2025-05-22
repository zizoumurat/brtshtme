using BritishTime.Domain.Dtos;
using BritishTime.Domain.Entities;
using System.Linq.Expressions;

namespace BritishTime.Domain.Repositories.LessonSessionTemplates;

public interface IQueryLessonSessionTemplateRepository
{
    Task<bool> IsExistingAsync(Expression<Func<LessonSessionTemplate, bool>> predicate);
    IQueryable<LessonSessionTemplate> GetList(Expression<Func<LessonSessionTemplate, bool>> expression, bool isTracking = false);
    Task<LessonSessionTemplateDto> GetByIdAsync(Guid id);
}
