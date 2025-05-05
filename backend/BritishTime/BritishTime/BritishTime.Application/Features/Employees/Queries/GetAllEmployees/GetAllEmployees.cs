using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;
using MediatR;


namespace BritishTime.Application.Features.EmployeesFeatures.Queries.GetAllEmployees;

public sealed record GetAllEmployeesQuery
    (EmployeeFilterDto filter, PageRequest pagination) : IRequest<GetAllEmployeesQueryResponse>;
