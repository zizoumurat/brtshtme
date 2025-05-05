using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;
using MediatR;


namespace BritishTime.Application.Features.EmployeesFeatures.Queries.GetEmployeeList;

public sealed record GetEmployeeListQuery(Guid BranchId) : IRequest<GetEmployeeListQueryResponse>;
