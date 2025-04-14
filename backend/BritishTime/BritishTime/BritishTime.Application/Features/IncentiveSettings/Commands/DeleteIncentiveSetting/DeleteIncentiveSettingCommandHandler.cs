using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.IncentiveSettings.Commands.DeleteIncentiveSetting;

public class DeleteIncentiveSettingCommandHandler : IRequestHandler<DeleteIncentiveSettingCommand, DeleteIncentiveSettingCommandResponse>
{
    private readonly IIncentiveSettingService _IncentiveSettingService;

    public DeleteIncentiveSettingCommandHandler(IIncentiveSettingService IncentiveSettingService)
    {
        _IncentiveSettingService = IncentiveSettingService;
    }

    public async Task<DeleteIncentiveSettingCommandResponse> Handle(DeleteIncentiveSettingCommand request, CancellationToken cancellationToken)
    {
        await _IncentiveSettingService.DeleteAsync(request.Id);

        return new("deleted");
    }
}
