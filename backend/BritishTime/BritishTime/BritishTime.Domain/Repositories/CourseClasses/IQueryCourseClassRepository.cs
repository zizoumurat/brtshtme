using BritishTime.Domain.Dtos;
using BritishTime.Domain.Entities;
using BritishTime.Domain.Pagination;
using System.Linq.Expressions;

namespace BritishTime.Domain.Repositories.CourseClasses;

public interface IQueryCourseClassRepository
{
    Task<bool> IsExistingAsync(Expression<Func<CourseClass, bool>> predicate);
    Task<PaginatedList<CourseClassDto>> GetAllAsync(CourseClassFilterDto filter, PageRequest pagination);
    IQueryable<CourseClass> GetList(Expression<Func<CourseClass, bool>> expression, bool isTracking = false);
    Task<CourseClassDto> GetByIdAsync(Guid id);
}
