using BritishTime.Domain.Dtos;
using MediatR;

namespace BritishTime.Application.Features.Employees.Commands.DeleteEmployee;

public sealed record DeleteEmployeeCommand(Guid Id) : IRequest<DeleteEmployeeCommandResponse>;