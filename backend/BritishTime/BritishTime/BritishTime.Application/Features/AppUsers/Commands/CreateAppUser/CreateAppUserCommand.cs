using BritishTime.Domain.Dtos;
using MediatR;

namespace BritishTime.Application.Features.AppUsers.Commands.CreateAppUser;

public sealed record CreateAppUserCommand(AppUserCreateDto AppUser) : IRequest<CreateAppUserCommandResponse>;