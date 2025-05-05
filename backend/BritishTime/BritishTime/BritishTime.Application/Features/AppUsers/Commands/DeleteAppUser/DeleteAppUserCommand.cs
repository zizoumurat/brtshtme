using BritishTime.Domain.Dtos;
using MediatR;

namespace BritishTime.Application.Features.AppUsers.Commands.DeleteAppUser;

public sealed record DeleteAppUserCommand(Guid Id) : IRequest<DeleteAppUserCommandResponse>;