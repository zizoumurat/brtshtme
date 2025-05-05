using BritishTime.Domain.Dtos;
using MediatR;

namespace BritishTime.Application.Features.Employees.Commands.UpdateEmployee;

public sealed record UpdateEmployeeCommand(EmployeeDto Employee) : IRequest<UpdateEmployeeCommandResponse>;