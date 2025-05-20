using BritishTime.Domain.Dtos;
using BritishTime.Domain.Entities;
using BritishTime.Domain.Pagination;
using System.Linq.Expressions;

namespace BritishTime.Domain.Repositories.Students;

public interface IQueryStudentRepository
{
    Task<bool> IsExistingAsync(Expression<Func<Student, bool>> predicate);
    Task<PaginatedList<StudentDto>> GetAllAsync(StudentFilterDto filter, PageRequest pagination);
    Task<StudentDto> GetByIdAsync(Guid id);
}
