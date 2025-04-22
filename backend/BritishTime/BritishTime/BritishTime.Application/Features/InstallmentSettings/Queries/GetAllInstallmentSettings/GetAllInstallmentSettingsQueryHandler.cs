
using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.InstallmentSettingsFeatures.Queries.GetAllInstallmentSettings;

public sealed class GetAllInstallmentSettingsQueryHandler : IRequestHandler<GetAllInstallmentSettingsQuery, GetAllInstallmentSettingsQueryResponse>
{
    private readonly IInstallmentSettingService _InstallmentSettingService;

    public GetAllInstallmentSettingsQueryHandler(IInstallmentSettingService InstallmentSettingService)
    {
        _InstallmentSettingService = InstallmentSettingService;
    }
    public async Task<GetAllInstallmentSettingsQueryResponse> Handle(GetAllInstallmentSettingsQuery request, CancellationToken cancellationToken)
    {
        var result = await _InstallmentSettingService.GetAllAsync(request.filter, request.pagination);

        return new(result);
    }
}