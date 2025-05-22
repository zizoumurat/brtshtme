using BritishTime.Domain.Dtos;
using BritishTime.Domain.Entities;
using BritishTime.Domain.Pagination;

namespace BritishTime.Application.Services.Abstract;

public interface ICourseClassService
{
    Task<PaginatedList<CourseClassDto>> GetAllAsync(CourseClassFilterDto filter, PageRequest pagination);
    Task<CourseClassDto> AddAsync(CourseClassCreateDto CourseClassDto);
    Task<CourseClassDto> UpdateAsync(CourseClassDto CourseClassDto);
    Task<bool> DeleteAsync(Guid id);
    Task<DateTime> CalculateEndDate(DateTime StartDate, Guid LessonScheduleId);
    Task<bool> IsClassroomScheduleConflictAsync(Guid classroomId, DateTime newStartDate, DateTime newEndDate, List<DayOfWeek> newLessonDays, TimeOnly newStartTime, int dayHour);
    Task<List<LessonSession>> GenerateLessonSessionAsync(Guid classId, Dictionary<DayOfWeek, Guid> programDaysWithTeachers);
    Task<List<LessonSessionListDto>> GetLessonSessionListByCourseClass(Guid courseClassId);
    Task<List<LessonSessionListDto>> GetLessonSessionListByTeacher(Guid employeeId);
}
