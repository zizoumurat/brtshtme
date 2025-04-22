using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.InstallmentSettings.Commands.DeleteInstallmentSetting;

public class DeleteInstallmentSettingCommandHandler : IRequestHandler<DeleteInstallmentSettingCommand, DeleteInstallmentSettingCommandResponse>
{
    private readonly IInstallmentSettingService _InstallmentSettingService;

    public DeleteInstallmentSettingCommandHandler(IInstallmentSettingService InstallmentSettingService)
    {
        _InstallmentSettingService = InstallmentSettingService;
    }

    public async Task<DeleteInstallmentSettingCommandResponse> Handle(DeleteInstallmentSettingCommand request, CancellationToken cancellationToken)
    {
        await _InstallmentSettingService.DeleteAsync(request.Id);

        return new("deleted");
    }
}
