using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;

namespace BritishTime.Domain.Repositories.Branches;

public interface IQueryBranchRepository
{
    Task<PaginatedList<BranchDto>> GetAllAsync(BranchFilterDto filter, PageRequest pagination);
    Task<BranchDto> GetByIdAsync(Guid id);
}
