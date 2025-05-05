using BritishTime.Domain.Dtos;
using BritishTime.Domain.Entities;
using BritishTime.Domain.Pagination;
using System.Linq.Expressions;

namespace BritishTime.Domain.Repositories.Employees;

public interface IQueryEmployeeRepository
{
    Task<PaginatedList<EmployeeDto>> GetAllAsync(EmployeeFilterDto filter, PageRequest pagination);
    IQueryable<Employee> GetAllAsync(Expression<Func<Employee, bool>> expression, bool isTracking = false);
    Task<List<SelectListDto>> GetListAsync(Guid BranchId);
    Task<EmployeeDto> GetByIdAsync(Guid id);
    Task<bool> IsExistingAsync(Expression<Func<Employee, bool>> predicate);
}
