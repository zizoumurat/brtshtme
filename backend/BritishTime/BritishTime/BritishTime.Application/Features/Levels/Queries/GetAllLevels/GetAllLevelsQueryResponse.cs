
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;

namespace BritishTime.Application.Features.LevelsFeatures.Queries.GetAllLevels;
public sealed record GetAllLevelsQueryResponse(PaginatedList<LevelDto> result);
