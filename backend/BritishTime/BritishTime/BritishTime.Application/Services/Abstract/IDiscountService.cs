using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;

namespace BritishTime.Application.Services.Abstract;

public interface IDiscountService
{
    Task<PaginatedList<DiscountDto>> GetAllAsync(DiscountFilterDto filter, PageRequest pagination);
    Task<DiscountDto> AddAsync(DiscountCreateDto DiscountDto);
    Task<DiscountDto> UpdateAsync(DiscountDto DiscountDto);
    Task<bool> DeleteAsync(Guid id);
}
