using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;
using System.Linq.Expressions;

namespace BritishTime.Application.Services.Abstract;

public interface IEmployeeService
{
    Task<PaginatedList<EmployeeDto>> GetAllAsync(EmployeeFilterDto filter, PageRequest pagination);
    Task<List<SelectListDto>> GetListAsync(Guid BranchId);
    Task<EmployeeDto> AddAsync(EmployeeCreateDto EmployeeDto);
    Task<EmployeeDto> UpdateAsync(EmployeeDto EmployeeDto);
    Task<bool> DeleteAsync(Guid id);
}
