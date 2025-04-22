using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.BranchPricingSettings.Commands.CreateBranchPricingSetting;

public class CreateBranchPricingSettingCommandHandler : IRequestHandler<CreateBranchPricingSettingCommand, CreateBranchPricingSettingCommandResponse>
{
    private readonly IBranchPricingSettingService _BranchPricingSettingService;

    public CreateBranchPricingSettingCommandHandler(IBranchPricingSettingService BranchPricingSettingService)
    {
        _BranchPricingSettingService = BranchPricingSettingService;
    }

    public async Task<CreateBranchPricingSettingCommandResponse> Handle(CreateBranchPricingSettingCommand request, CancellationToken cancellationToken)
    {
        await _BranchPricingSettingService.AddOrUpdateAsync(request.BranchPricingSetting);
        return new("added");
    }
}
