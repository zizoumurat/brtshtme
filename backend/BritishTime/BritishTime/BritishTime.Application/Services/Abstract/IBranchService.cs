using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;

namespace BritishTime.Application.Services.Abstract;
public interface IBranchService
{
    Task<PaginatedList<BranchDto>> GetAllAsync(BranchFilterDto filter, PageRequest pagination);
    Task<BranchDto> AddAsync(BranchCreateDto branchDto);
    Task<BranchDto> UpdateAsync(BranchDto branchDto);
    Task<bool> DeleteAsync(Guid id);
}
