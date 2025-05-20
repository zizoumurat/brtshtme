using BritishTime.Domain.Dtos;
using BritishTime.Domain.Entities;
using BritishTime.Domain.Pagination;
using System.Linq.Expressions;

namespace BritishTime.Domain.Repositories.Installments;

public interface IQueryInstallmentRepository
{
    Task<bool> IsExistingAsync(Expression<Func<Installment, bool>> predicate);
    Task<PaginatedList<InstallmentDto>> GetAllAsync(InstallmentFilterDto filter, PageRequest pagination);
    Task<InstallmentDto> GetByIdAsync(Guid id);
}
