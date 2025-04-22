using BritishTime.Domain.Dtos;

namespace BritishTime.Domain.Repositories.BranchPricingSettings;

public interface IQueryBranchPricingSettingRepository
{
    Task<BranchPricingSettingDto> GetByBranchId(Guid branchId);
}
