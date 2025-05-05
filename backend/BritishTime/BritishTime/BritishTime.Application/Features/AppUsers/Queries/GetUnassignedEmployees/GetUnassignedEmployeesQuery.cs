using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;
using MediatR;


namespace BritishTime.Application.Features.AppUsersFeatures.Queries.GetUnassignedEmployees;

public sealed record GetUnassignedEmployeesQuery
    (Guid BranchId) : IRequest<GetUnassignedEmployeesQueryResponse>;
