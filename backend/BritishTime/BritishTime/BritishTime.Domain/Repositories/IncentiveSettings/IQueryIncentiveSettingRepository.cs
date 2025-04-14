using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;

namespace BritishTime.Domain.Repositories.IncentiveSettings;

public interface IQueryIncentiveSettingRepository
{
    Task<PaginatedList<IncentiveSettingDto>> GetAllAsync(IncentiveSettingFilterDto filter, PageRequest pagination);
    Task<IncentiveSettingDto> GetByIdAsync(Guid id);
}
