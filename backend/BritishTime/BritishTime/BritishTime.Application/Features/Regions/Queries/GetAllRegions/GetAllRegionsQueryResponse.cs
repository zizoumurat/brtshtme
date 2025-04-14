
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;

namespace BritishTime.Application.Features.RegionsFeatures.Queries.GetAllRegions;
public sealed record GetAllRegionsQueryResponse(PaginatedList<RegionDto> result);
