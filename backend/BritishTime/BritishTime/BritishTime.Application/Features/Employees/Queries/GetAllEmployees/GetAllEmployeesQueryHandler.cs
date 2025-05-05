
using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.EmployeesFeatures.Queries.GetAllEmployees;

public sealed class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, GetAllEmployeesQueryResponse>
{
    private readonly IEmployeeService _EmployeeService;

    public GetAllEmployeesQueryHandler(IEmployeeService EmployeeService)
    {
        _EmployeeService = EmployeeService;
    }
    public async Task<GetAllEmployeesQueryResponse> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
    {
        var result = await _EmployeeService.GetAllAsync(request.filter, request.pagination);

        return new(result);
    }
}