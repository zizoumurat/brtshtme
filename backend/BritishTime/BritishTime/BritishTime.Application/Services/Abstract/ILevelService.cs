using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;

namespace BritishTime.Application.Services.Abstract;

public interface ILevelService
{
    Task<PaginatedList<LevelDto>> GetAllAsync(LevelFilterDto filter, PageRequest pagination);
    Task<LevelDto> AddAsync(LevelCreateDto LevelDto);
    Task<LevelDto> UpdateAsync(LevelDto LevelDto);
    Task<bool> DeleteAsync(Guid id);
}
