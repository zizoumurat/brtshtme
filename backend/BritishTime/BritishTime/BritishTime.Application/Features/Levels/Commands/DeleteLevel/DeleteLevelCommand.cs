using BritishTime.Domain.Dtos;
using MediatR;

namespace BritishTime.Application.Features.Levels.Commands.DeleteLevel;

public sealed record DeleteLevelCommand(Guid Id) : IRequest<DeleteLevelCommandResponse>;