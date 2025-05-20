using BritishTime.Domain.Entities;

namespace BritishTime.Domain.Repositories.CourseClasses;

public interface ICommandCourseClassRepository
{
    Task AddAsync(CourseClass CourseClass);
    Task UpdateAsync(CourseClass CourseClass);
    Task DeleteAsync(CourseClass CourseClass);
}