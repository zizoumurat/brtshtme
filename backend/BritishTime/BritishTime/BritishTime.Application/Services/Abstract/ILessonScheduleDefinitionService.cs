using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;

namespace BritishTime.Application.Services.Abstract;

public interface ILessonScheduleDefinitionService
{
    Task<PaginatedList<LessonScheduleDefinitionDto>> GetAllAsync(LessonScheduleDefinitionFilterDto filter, PageRequest pagination);
    Task<LessonScheduleDefinitionDto> AddAsync(LessonScheduleDefinitionCreateDto LessonScheduleDefinitionDto);
    Task<LessonScheduleDefinitionDto> UpdateAsync(LessonScheduleDefinitionCreateDto LessonScheduleDefinitionDto);
    Task<bool> ExistAsync(LessonScheduleDefinitionCreateDto lessonScheduleDefinition);
    Task<bool> ExistSchedule(string Schedule, Guid BranchId, Guid? Id = null);
    Task<bool> DeleteAsync(Guid id);
    string GenerateScheduleCode(LessonScheduleDefinitionCreateDto lessonScheduleDefinition);
}
