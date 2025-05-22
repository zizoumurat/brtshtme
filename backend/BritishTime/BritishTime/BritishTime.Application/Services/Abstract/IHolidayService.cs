namespace BritishTime.Application.Services.Abstract;

public interface IHolidayService
{
    Task<List<DateTime>> GetHolidaysAsync(int year);
    Task<List<DateTime>> GetCourseHolidaysAsync(Guid BranchId, Guid CourseClassId, DateTime StartDate);
}