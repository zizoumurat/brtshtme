
using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.IncentiveSettingsFeatures.Queries.GetAllIncentiveSettings;

public sealed class GetAllIncentiveSettingsQueryHandler : IRequestHandler<GetAllIncentiveSettingsQuery, GetAllIncentiveSettingsQueryResponse>
{
    private readonly IIncentiveSettingService _IncentiveSettingService;

    public GetAllIncentiveSettingsQueryHandler(IIncentiveSettingService IncentiveSettingService)
    {
        _IncentiveSettingService = IncentiveSettingService;
    }
    public async Task<GetAllIncentiveSettingsQueryResponse> Handle(GetAllIncentiveSettingsQuery request, CancellationToken cancellationToken)
    {
        var result = await _IncentiveSettingService.GetAllAsync(request.filter, request.pagination);

        return new(result);
    }
}