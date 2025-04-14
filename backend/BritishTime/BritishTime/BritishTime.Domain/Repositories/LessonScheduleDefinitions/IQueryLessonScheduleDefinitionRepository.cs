using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;

namespace BritishTime.Domain.Repositories.LessonScheduleDefinitions;

public interface IQueryLessonScheduleDefinitionRepository
{
    Task<PaginatedList<LessonScheduleDefinitionDto>> GetAllAsync(LessonScheduleDefinitionFilterDto filter, PageRequest pagination);
    Task<LessonScheduleDefinitionDto> GetByIdAsync(Guid id);
}
