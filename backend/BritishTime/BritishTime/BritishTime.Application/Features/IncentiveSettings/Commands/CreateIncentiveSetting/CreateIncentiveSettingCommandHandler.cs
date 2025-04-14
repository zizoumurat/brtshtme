using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.IncentiveSettinges.Commands.CreateIncentiveSetting;

public class CreateIncentiveSettingCommandHandler : IRequestHandler<CreateIncentiveSettingCommand, CreateIncentiveSettingCommandResponse>
{
    private readonly IIncentiveSettingService _IncentiveSettingService;

    public CreateIncentiveSettingCommandHandler(IIncentiveSettingService IncentiveSettingService)
    {
        _IncentiveSettingService = IncentiveSettingService;
    }

    public async Task<CreateIncentiveSettingCommandResponse> Handle(CreateIncentiveSettingCommand request, CancellationToken cancellationToken)
    {
        await _IncentiveSettingService.AddAsync(request.IncentiveSetting);
        return new("added");
    }
}
