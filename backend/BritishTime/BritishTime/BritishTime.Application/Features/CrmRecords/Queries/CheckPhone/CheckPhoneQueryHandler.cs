
using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.CrmRecordsFeatures.Queries.CheckPhone;

public sealed class CheckPhoneQueryHandler : IRequestHandler<CheckPhoneQuery, CheckPhoneQueryResponse>
{
    private readonly ICrmRecordService _crmRecordService;

    public CheckPhoneQueryHandler(ICrmRecordService CrmRecordService)
    {
        _crmRecordService = CrmRecordService;
    }
    public async Task<CheckPhoneQueryResponse> Handle(CheckPhoneQuery request, CancellationToken cancellationToken)
    {
        var result = await _crmRecordService.CheckPhone(request.Phone);

        return new(result);
    }
}