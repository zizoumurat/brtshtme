using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;

namespace BritishTime.Domain.Repositories.Regions;

public interface IQueryRegionRepository
{
    Task<PaginatedList<RegionDto>> GetAllAsync(RegionFilterDto filter, PageRequest pagination);
    Task<RegionDto> GetByIdAsync(Guid id);
}
