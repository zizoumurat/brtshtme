using BritishTime.Domain.Dtos;
using MediatR;

namespace BritishTime.Application.Features.Employees.Commands.CreateEmployee;

public sealed record CreateEmployeeCommand(EmployeeCreateDto Employee) : IRequest<CreateEmployeeCommandResponse>;