using BritishTime.Domain.Dtos;

namespace BritishTime.Application.Services.Abstract;
public interface IBranchPricingSettingService
{
    Task<BranchPricingSettingDto> GetBranchPricingSettingByBranchId(Guid BranchId);
    Task<BranchPricingSettingDto> AddOrUpdateAsync(BranchPricingSettingDto BranchPricingSettingDto);
}
