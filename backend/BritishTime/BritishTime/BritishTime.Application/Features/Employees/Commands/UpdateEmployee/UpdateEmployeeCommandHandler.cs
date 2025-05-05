using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.Employees.Commands.UpdateEmployee;

public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, UpdateEmployeeCommandResponse>
{
    private readonly IEmployeeService _EmployeeService;

    public UpdateEmployeeCommandHandler(IEmployeeService EmployeeService)
    {
        _EmployeeService = EmployeeService;
    }

    public async Task<UpdateEmployeeCommandResponse> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        await _EmployeeService.UpdateAsync(request.Employee);
        return new("updated");
    }
}
