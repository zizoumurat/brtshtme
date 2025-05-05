
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;

namespace BritishTime.Application.Features.RegionsFeatures.Queries.GetRegionList;
public sealed record GetRegionListQueryResponse(List<SelectListDto> result);
