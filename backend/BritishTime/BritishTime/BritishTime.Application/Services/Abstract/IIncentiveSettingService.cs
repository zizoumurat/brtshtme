using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;

namespace BritishTime.Application.Services.Abstract;

public interface IIncentiveSettingService
{
    Task<PaginatedList<IncentiveSettingDto>> GetAllAsync(IncentiveSettingFilterDto filter, PageRequest pagination);
    Task<IncentiveSettingDto> AddAsync(IncentiveSettingCreateDto IncentiveSettingDto);
    Task<IncentiveSettingDto> UpdateAsync(IncentiveSettingDto IncentiveSettingDto);
    Task<bool> DeleteAsync(Guid id);
}
