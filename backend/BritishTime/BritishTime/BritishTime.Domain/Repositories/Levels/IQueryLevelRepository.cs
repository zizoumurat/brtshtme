using BritishTime.Domain.Dtos;
using BritishTime.Domain.Entities;
using BritishTime.Domain.Pagination;
using System.Linq.Expressions;

namespace BritishTime.Domain.Repositories.Levels;

public interface IQueryLevelRepository
{
    Task<bool> IsExistingAsync(Expression<Func<Level, bool>> predicate);
    Task<PaginatedList<LevelDto>> GetAllAsync(LevelFilterDto filter, PageRequest pagination);
    Task<LevelDto> GetByIdAsync(Guid id);
}
