using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.Campaigns.Commands.DeleteCampaign;

public class DeleteCampaignCommandHandler : IRequestHandler<DeleteCampaignCommand, DeleteCampaignCommandResponse>
{
    private readonly ICampaignService _CampaignService;

    public DeleteCampaignCommandHandler(ICampaignService CampaignService)
    {
        _CampaignService = CampaignService;
    }

    public async Task<DeleteCampaignCommandResponse> Handle(DeleteCampaignCommand request, CancellationToken cancellationToken)
    {
        await _CampaignService.DeleteAsync(request.Id);

        return new("deleted");
    }
}
