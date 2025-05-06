
using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.CampaignsFeatures.Queries.GetOpenCalls;

public sealed class GetOpenCallsQueryHandler : IRequestHandler<GetOpenCallsQuery, GetOpenCallsQueryResponse>
{
    private readonly ICrmRecordActionService _crmRecordActionService;

    public GetOpenCallsQueryHandler(ICrmRecordActionService crmRecordActionService)
    {
        _crmRecordActionService = crmRecordActionService;
    }
    public async Task<GetOpenCallsQueryResponse> Handle(GetOpenCallsQuery request, CancellationToken cancellationToken)
    {
        var result = await _crmRecordActionService.GetOpenCallsAsync();

        return new(result);
    }
}