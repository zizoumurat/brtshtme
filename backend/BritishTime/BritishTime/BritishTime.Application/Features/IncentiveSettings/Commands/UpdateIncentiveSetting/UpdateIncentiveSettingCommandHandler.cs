using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.IncentiveSettings.Commands.UpdateIncentiveSetting;

public class UpdateIncentiveSettingCommandHandler : IRequestHandler<UpdateIncentiveSettingCommand, UpdateIncentiveSettingCommandResponse>
{
    private readonly IIncentiveSettingService _IncentiveSettingService;

    public UpdateIncentiveSettingCommandHandler(IIncentiveSettingService IncentiveSettingService)
    {
        _IncentiveSettingService = IncentiveSettingService;
    }

    public async Task<UpdateIncentiveSettingCommandResponse> Handle(UpdateIncentiveSettingCommand request, CancellationToken cancellationToken)
    {
        await _IncentiveSettingService.UpdateAsync(request.IncentiveSetting);
        return new("updated");
    }
}
