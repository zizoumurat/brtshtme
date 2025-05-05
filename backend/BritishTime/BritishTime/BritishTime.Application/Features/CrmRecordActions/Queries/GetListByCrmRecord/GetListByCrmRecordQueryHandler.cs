
using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.CampaignsFeatures.Queries.GetListByCrmRecord;

public sealed class GetListByCrmRecordQueryHandler : IRequestHandler<GetListByCrmRecordQuery, GetListByCrmRecordQueryResponse>
{
    private readonly ICrmRecordActionService _crmRecordActionService;

    public GetListByCrmRecordQueryHandler(ICrmRecordActionService crmRecordActionService)
    {
        _crmRecordActionService = crmRecordActionService;
    }
    public async Task<GetListByCrmRecordQueryResponse> Handle(GetListByCrmRecordQuery request, CancellationToken cancellationToken)
    {
        var result = await _crmRecordActionService.GetListByCrmRecord(request.CrmRecordId);

        return new(result);
    }
}