
using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.CrmRecordsFeatures.Queries.GetAllCrmRecords;

public sealed class GetAllCrmRecordsQueryHandler : IRequestHandler<GetAllCrmRecordsQuery, GetAllCrmRecordsQueryResponse>
{
    private readonly ICrmRecordService _CrmRecordService;

    public GetAllCrmRecordsQueryHandler(ICrmRecordService CrmRecordService)
    {
        _CrmRecordService = CrmRecordService;
    }
    public async Task<GetAllCrmRecordsQueryResponse> Handle(GetAllCrmRecordsQuery request, CancellationToken cancellationToken)
    {
        var result = await _CrmRecordService.GetAllAsync(request.filter, request.pagination);

        return new(result);
    }
}