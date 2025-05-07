
using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.CrmRecordActionsFeatures.Queries.GetAllCrmRecordActions;

public sealed class GetAllCrmRecordActionsQueryHandler : IRequestHandler<GetAllCrmRecordActionsQuery, GetAllCrmRecordActionsQueryResponse>
{
    private readonly ICrmRecordActionService _crmRecordActionservice;

    public GetAllCrmRecordActionsQueryHandler(ICrmRecordActionService crmRecordActionservice)
    {
        _crmRecordActionservice = crmRecordActionservice;
    }
    public async Task<GetAllCrmRecordActionsQueryResponse> Handle(GetAllCrmRecordActionsQuery request, CancellationToken cancellationToken)
    {
        var result = await _crmRecordActionservice.GetAllAsync(request.filter, request.pagination);

        return new(result);
    }
}