using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;

namespace BritishTime.Application.Services.Abstract;

public interface IAppUserService
{
    Task<PaginatedList<AppUserDto>> GetAllAsync(AppUserFilterDto filter, PageRequest pagination);
    Task<bool> AddAsync(AppUserCreateDto AppUserDto);
    Task<bool> DeleteAsync(Guid id);
    Task<List<UnassignedEmployeeDto>> GetUnassignedEmployees(Guid BranchId);
}
