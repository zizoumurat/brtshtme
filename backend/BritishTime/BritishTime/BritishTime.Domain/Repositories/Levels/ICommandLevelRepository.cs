using BritishTime.Domain.Entities;

namespace BritishTime.Domain.Repositories.Levels;

public interface ICommandLevelRepository
{
    Task AddAsync(Level Level);
    Task UpdateAsync(Level Level);
    Task DeleteAsync(Level Level);
}