
using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.CampaignsFeatures.Queries.GetAllCampaigns;

public sealed class GetAllCampaignsQueryHandler : IRequestHandler<GetAllCampaignsQuery, GetAllCampaignsQueryResponse>
{
    private readonly ICampaignService _CampaignService;

    public GetAllCampaignsQueryHandler(ICampaignService CampaignService)
    {
        _CampaignService = CampaignService;
    }
    public async Task<GetAllCampaignsQueryResponse> Handle(GetAllCampaignsQuery request, CancellationToken cancellationToken)
    {
        var result = await _CampaignService.GetAllAsync(request.filter, request.pagination);

        return new(result);
    }
}