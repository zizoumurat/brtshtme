
using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.CampaignsFeatures.Queries.GetValidCallsByDate;

public sealed class GetValidCallsByDateQueryHandler : IRequestHandler<GetValidCallsByDateQuery, GetValidCallsByDateQueryResponse>
{
    private readonly ICrmRecordActionService _crmRecordActionService;

    public GetValidCallsByDateQueryHandler(ICrmRecordActionService crmRecordActionService)
    {
        _crmRecordActionService = crmRecordActionService;
    }
    public async Task<GetValidCallsByDateQueryResponse> Handle(GetValidCallsByDateQuery request, CancellationToken cancellationToken)
    {
        var result = await _crmRecordActionService.GetValidCallsByDateAsync(request.Date);

        return new(result);
    }
}