
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;

namespace BritishTime.Application.Features.AppUsersFeatures.Queries.GetAllAppUsers;
public sealed record GetAllAppUsersQueryResponse(PaginatedList<AppUserDto> result);
