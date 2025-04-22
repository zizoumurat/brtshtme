using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.InstallmentSettings.Commands.UpdateInstallmentSetting;

public class UpdateInstallmentSettingCommandHandler : IRequestHandler<UpdateInstallmentSettingCommand, UpdateInstallmentSettingCommandResponse>
{
    private readonly IInstallmentSettingService _InstallmentSettingService;

    public UpdateInstallmentSettingCommandHandler(IInstallmentSettingService InstallmentSettingService)
    {
        _InstallmentSettingService = InstallmentSettingService;
    }

    public async Task<UpdateInstallmentSettingCommandResponse> Handle(UpdateInstallmentSettingCommand request, CancellationToken cancellationToken)
    {
        await _InstallmentSettingService.UpdateAsync(request.InstallmentSetting);
        return new("updated");
    }
}
