
using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.EmployeesFeatures.Queries.GetEmployeeList;

public sealed class GetEmployeeListQueryHandler : IRequestHandler<GetEmployeeListQuery, GetEmployeeListQueryResponse>
{
    private readonly IEmployeeService _employeeService;

    public GetEmployeeListQueryHandler(IEmployeeService EmployeeService)
    {
        _employeeService = EmployeeService;
    }
    public async Task<GetEmployeeListQueryResponse> Handle(GetEmployeeListQuery request, CancellationToken cancellationToken)
    {
        var result = await _employeeService.GetListAsync(request.BranchId);

        return new(result);
    }
}