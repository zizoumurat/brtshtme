using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;

namespace BritishTime.Application.Services.Abstract;

public interface ICourseClassService
{
    Task<PaginatedList<CourseClassDto>> GetAllAsync(CourseClassFilterDto filter, PageRequest pagination);
    Task<CourseClassDto> AddAsync(CourseClassCreateDto CourseClassDto);
    Task<CourseClassDto> UpdateAsync(CourseClassDto CourseClassDto);
    Task<bool> DeleteAsync(Guid id);
    Task<DateTime> CalculateEndDate(DateTime StartDate, Guid LessonScheduleId);
}
