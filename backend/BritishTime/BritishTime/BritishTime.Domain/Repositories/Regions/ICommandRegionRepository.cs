using BritishTime.Domain.Entities;

namespace BritishTime.Domain.Repositories.Regions;

public interface ICommandRegionRepository
{
    Task AddAsync(Region Region);
    Task UpdateAsync(Region Region);
    Task DeleteAsync(Region Region);
}