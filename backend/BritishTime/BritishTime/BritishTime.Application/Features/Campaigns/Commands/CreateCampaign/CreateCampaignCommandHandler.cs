using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.Campaigns.Commands.CreateCampaign;

public class CreateCampaignCommandHandler : IRequestHandler<CreateCampaignCommand, CreateCampaignCommandResponse>
{
    private readonly ICampaignService _CampaignService;

    public CreateCampaignCommandHandler(ICampaignService CampaignService)
    {
        _CampaignService = CampaignService;
    }

    public async Task<CreateCampaignCommandResponse> Handle(CreateCampaignCommand request, CancellationToken cancellationToken)
    {
        await _CampaignService.AddAsync(request.Campaign);
        return new("added");
    }
}
