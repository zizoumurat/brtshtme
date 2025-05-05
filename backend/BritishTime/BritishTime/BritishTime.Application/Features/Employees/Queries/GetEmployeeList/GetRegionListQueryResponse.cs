
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;

namespace BritishTime.Application.Features.EmployeesFeatures.Queries.GetEmployeeList;
public sealed record GetEmployeeListQueryResponse(List<SelectListDto> result);
