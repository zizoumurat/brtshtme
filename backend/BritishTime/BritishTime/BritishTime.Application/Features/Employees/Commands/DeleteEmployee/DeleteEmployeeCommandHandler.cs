using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.Employees.Commands.DeleteEmployee;

public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, DeleteEmployeeCommandResponse>
{
    private readonly IEmployeeService _EmployeeService;

    public DeleteEmployeeCommandHandler(IEmployeeService EmployeeService)
    {
        _EmployeeService = EmployeeService;
    }

    public async Task<DeleteEmployeeCommandResponse> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        await _EmployeeService.DeleteAsync(request.Id);

        return new("deleted");
    }
}
