using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;
using MediatR;


namespace BritishTime.Application.Features.RegionsFeatures.Queries.GetAllRegions;

public sealed record GetAllRegionsQuery
    (RegionFilterDto filter, PageRequest pagination) : IRequest<GetAllRegionsQueryResponse>;
