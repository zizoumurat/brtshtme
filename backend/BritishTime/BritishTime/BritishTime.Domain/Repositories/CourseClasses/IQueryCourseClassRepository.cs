using BritishTime.Domain.Dtos;
using BritishTime.Domain.Entities;
using BritishTime.Domain.Pagination;
using System.Linq.Expressions;

namespace BritishTime.Domain.Repositories.CourseClasses;

public interface IQueryCourseClassRepository
{
    Task<bool> IsExistingAsync(Expression<Func<CourseClass, bool>> predicate);
    Task<PaginatedList<CourseClassDto>> GetAllAsync(CourseClassFilterDto filter, PageRequest pagination);
    Task<CourseClassDto> GetByIdAsync(Guid id);
}
