using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;
using MediatR;


namespace BritishTime.Application.Features.AppUsersFeatures.Queries.GetAllAppUsers;

public sealed record GetAllAppUsersQuery
    (AppUserFilterDto filter, PageRequest pagination) : IRequest<GetAllAppUsersQueryResponse>;
