using BritishTime.Domain.Entities;

namespace BritishTime.Domain.Repositories.BranchPricingSettings;

public interface ICommandBranchPricingSettingRepository
{
    Task AddAsync(BranchPricingSetting BranchPricingSetting);
    Task UpdateAsync(BranchPricingSetting BranchPricingSetting);
}