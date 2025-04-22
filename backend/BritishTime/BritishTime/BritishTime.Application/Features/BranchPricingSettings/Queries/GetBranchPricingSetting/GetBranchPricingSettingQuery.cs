using MediatR;

namespace BritishTime.Application.Features.BranchPricingSettings.Queries.GetBranchPricingSetting;

public sealed record GetBranchPricingSettingQuery(Guid BranchId) : IRequest<GetBranchPricingSettingQueryResponse>;
