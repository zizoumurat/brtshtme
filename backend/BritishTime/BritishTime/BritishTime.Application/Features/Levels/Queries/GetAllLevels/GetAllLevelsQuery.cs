using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;
using MediatR;


namespace BritishTime.Application.Features.LevelsFeatures.Queries.GetAllLevels;

public sealed record GetAllLevelsQuery
    (LevelFilterDto filter, PageRequest pagination) : IRequest<GetAllLevelsQueryResponse>;
