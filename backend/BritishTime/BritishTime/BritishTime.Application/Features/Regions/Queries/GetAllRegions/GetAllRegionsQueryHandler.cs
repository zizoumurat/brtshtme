
using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.RegionsFeatures.Queries.GetAllRegions;

public sealed class GetAllRegionsQueryHandler : IRequestHandler<GetAllRegionsQuery, GetAllRegionsQueryResponse>
{
    private readonly IRegionService _RegionService;

    public GetAllRegionsQueryHandler(IRegionService RegionService)
    {
        _RegionService = RegionService;
    }
    public async Task<GetAllRegionsQueryResponse> Handle(GetAllRegionsQuery request, CancellationToken cancellationToken)
    {
        var result = await _RegionService.GetAllAsync(request.filter, request.pagination);

        return new(result);
    }
}