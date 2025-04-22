using BritishTime.Domain.Dtos;
using BritishTime.Domain.Entities;
using BritishTime.Domain.Pagination;
using System.Linq.Expressions;

namespace BritishTime.Domain.Repositories.LessonScheduleDefinitions;

public interface IQueryLessonScheduleDefinitionRepository
{
    Task<PaginatedList<LessonScheduleDefinitionDto>> GetAllAsync(LessonScheduleDefinitionFilterDto filter, PageRequest pagination);
    Task<LessonScheduleDefinitionDto> GetByIdAsync(Guid id);
    Task<bool> ExistsAsync(Expression<Func<LessonScheduleDefinition, bool>> predicate);
}
