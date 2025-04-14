using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;

namespace BritishTime.Application.Services.Abstract;

public interface IRegionService
{
    Task<PaginatedList<RegionDto>> GetAllAsync(RegionFilterDto filter, PageRequest pagination);
    Task<RegionDto> AddAsync(RegionCreateDto RegionDto);
    Task<RegionDto> UpdateAsync(RegionDto RegionDto);
    Task<bool> DeleteAsync(Guid id);
}
