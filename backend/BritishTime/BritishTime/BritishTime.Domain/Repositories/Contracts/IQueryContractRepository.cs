using BritishTime.Domain.Dtos;
using BritishTime.Domain.Entities;
using BritishTime.Domain.Pagination;
using System.Linq.Expressions;

namespace BritishTime.Domain.Repositories.Contracts;

public interface IQueryContractRepository
{
    Task<bool> IsExistingAsync(Expression<Func<Contract, bool>> predicate);
    Task<PaginatedList<ContractDto>> GetAllAsync(ContractFilterDto filter, PageRequest pagination);
    Task<ContractDto> GetByIdAsync(Guid id);
}
