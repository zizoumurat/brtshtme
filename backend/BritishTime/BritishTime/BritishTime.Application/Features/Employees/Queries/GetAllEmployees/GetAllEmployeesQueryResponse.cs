
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;

namespace BritishTime.Application.Features.EmployeesFeatures.Queries.GetAllEmployees;
public sealed record GetAllEmployeesQueryResponse(PaginatedList<EmployeeDto> result);
