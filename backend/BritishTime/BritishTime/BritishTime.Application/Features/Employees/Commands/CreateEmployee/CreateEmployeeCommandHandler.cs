using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.Employees.Commands.CreateEmployee;

public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, CreateEmployeeCommandResponse>
{
    private readonly IEmployeeService _EmployeeService;

    public CreateEmployeeCommandHandler(IEmployeeService EmployeeService)
    {
        _EmployeeService = EmployeeService;
    }

    public async Task<CreateEmployeeCommandResponse> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        await _EmployeeService.AddAsync(request.Employee);
        return new("added");
    }
}
