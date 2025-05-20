using BritishTime.Domain.Dtos;
using BritishTime.Domain.Entities;
using BritishTime.Domain.Pagination;
using System.Linq.Expressions;

namespace BritishTime.Domain.Repositories.Payments;

public interface IQueryPaymentRepository
{
    Task<bool> IsExistingAsync(Expression<Func<Payment, bool>> predicate);
    Task<PaginatedList<PaymentDto>> GetAllAsync(PaymentFilterDto filter, PageRequest pagination);
    Task<PaymentDto> GetByIdAsync(Guid id);
}
