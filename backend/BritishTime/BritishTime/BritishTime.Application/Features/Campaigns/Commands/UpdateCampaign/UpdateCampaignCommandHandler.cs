using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.Campaigns.Commands.UpdateCampaign;

public class UpdateCampaignCommandHandler : IRequestHandler<UpdateCampaignCommand, UpdateCampaignCommandResponse>
{
    private readonly ICampaignService _CampaignService;

    public UpdateCampaignCommandHandler(ICampaignService CampaignService)
    {
        _CampaignService = CampaignService;
    }

    public async Task<UpdateCampaignCommandResponse> Handle(UpdateCampaignCommand request, CancellationToken cancellationToken)
    {
        await _CampaignService.UpdateAsync(request.Campaign);
        return new("updated");
    }
}
