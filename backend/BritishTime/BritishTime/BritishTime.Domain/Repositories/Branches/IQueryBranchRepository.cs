using BritishTime.Domain.Dtos;
using BritishTime.Domain.Entities;
using BritishTime.Domain.Pagination;
using System.Linq.Expressions;

namespace BritishTime.Domain.Repositories.Branches;

public interface IQueryBranchRepository
{
    Task<PaginatedList<BranchDto>> GetAllAsync(BranchFilterDto filter, PageRequest pagination);
    IQueryable<Branch> GetList(Expression<Func<Branch, bool>> expression, bool isTracking = false);
    Task<BranchDto> GetByIdAsync(Guid id);
}
