using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;
using MediatR;


namespace BritishTime.Application.Features.RegionsFeatures.Queries.GetRegionList;

public sealed record GetRegionListQuery() : IRequest<GetRegionListQueryResponse>;
