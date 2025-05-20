
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;

namespace BritishTime.Application.Features.AppUsersFeatures.Queries.GetUnassignedEmployees;
public sealed record GetUnassignedEmployeesQueryResponse(List<UnassignedEmployeeDto> result);
