using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.InstallmentSettings.Commands.CreateInstallmentSetting;

public class CreateInstallmentSettingCommandHandler : IRequestHandler<CreateInstallmentSettingCommand, CreateInstallmentSettingCommandResponse>
{
    private readonly IInstallmentSettingService _InstallmentSettingService;

    public CreateInstallmentSettingCommandHandler(IInstallmentSettingService InstallmentSettingService)
    {
        _InstallmentSettingService = InstallmentSettingService;
    }

    public async Task<CreateInstallmentSettingCommandResponse> Handle(CreateInstallmentSettingCommand request, CancellationToken cancellationToken)
    {
        await _InstallmentSettingService.AddAsync(request.InstallmentSetting);
        return new("added");
    }
}
