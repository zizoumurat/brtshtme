using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;

namespace BritishTime.Application.Services.Abstract;

public interface ILessonScheduleDefinitionService
{
    Task<PaginatedList<LessonScheduleDefinitionDto>> GetAllAsync(LessonScheduleDefinitionFilterDto filter, PageRequest pagination);
    Task<LessonScheduleDefinitionDto> AddAsync(LessonScheduleDefinitionCreateDto LessonScheduleDefinitionDto);
    Task<LessonScheduleDefinitionDto> UpdateAsync(LessonScheduleDefinitionDto LessonScheduleDefinitionDto);
    Task<bool> DeleteAsync(Guid id);
}
