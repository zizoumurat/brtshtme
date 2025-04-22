using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;

namespace BritishTime.Domain.Repositories.Discounts;

public interface IQueryDiscountRepository
{
    Task<PaginatedList<DiscountDto>> GetAllAsync(DiscountFilterDto filter, PageRequest pagination);
    Task<DiscountDto> GetByIdAsync(Guid id);
}
