
using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.CrmRecordsFeatures.Queries.GetById;

public sealed class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, GetByIdQueryResponse>
{
    private readonly ICrmRecordService _CrmRecordService;

    public GetByIdQueryHandler(ICrmRecordService CrmRecordService)
    {
        _CrmRecordService = CrmRecordService;
    }
    public async Task<GetByIdQueryResponse> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _CrmRecordService.GetByIdAsync(request.Id);

        return new(result);
    }
}