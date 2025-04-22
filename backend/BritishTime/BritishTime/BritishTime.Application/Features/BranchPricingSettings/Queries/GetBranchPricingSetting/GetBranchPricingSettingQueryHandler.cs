using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.BranchPricingSettings.Queries.GetBranchPricingSetting;

public sealed class GetBranchPricingSettingQueryHandler : IRequestHandler<GetBranchPricingSettingQuery, GetBranchPricingSettingQueryResponse>
{
    private readonly IBranchPricingSettingService _branchPricingSettingService;

    public GetBranchPricingSettingQueryHandler(IBranchPricingSettingService branchPricingSettingService)
    {
        _branchPricingSettingService = branchPricingSettingService;
    }
    public async Task<GetBranchPricingSettingQueryResponse> Handle(GetBranchPricingSettingQuery request, CancellationToken cancellationToken)
    {
        var result = await _branchPricingSettingService.GetBranchPricingSettingByBranchId(request.BranchId);

        return new(result);
    }
}