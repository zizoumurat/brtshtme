using BritishTime.Domain.Entities;

namespace BritishTime.Domain.Repositories.IncentiveSettings;

public interface ICommandIncentiveSettingRepository
{
    Task AddAsync(IncentiveSetting IncentiveSetting);
    Task UpdateAsync(IncentiveSetting IncentiveSetting);
    Task DeleteAsync(IncentiveSetting IncentiveSetting);
}