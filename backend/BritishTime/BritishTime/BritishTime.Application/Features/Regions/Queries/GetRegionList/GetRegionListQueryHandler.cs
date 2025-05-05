
using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.RegionsFeatures.Queries.GetRegionList;

public sealed class GetRegionListQueryHandler : IRequestHandler<GetRegionListQuery, GetRegionListQueryResponse>
{
    private readonly IRegionService _RegionService;

    public GetRegionListQueryHandler(IRegionService RegionService)
    {
        _RegionService = RegionService;
    }
    public async Task<GetRegionListQueryResponse> Handle(GetRegionListQuery request, CancellationToken cancellationToken)
    {
        var result = await _RegionService.GetListAsync();

        return new(result);
    }
}