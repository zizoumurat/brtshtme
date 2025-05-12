using BritishTime.Domain.Dtos;
using MediatR;

namespace BritishTime.Application.Features.Levels.Commands.UpdateLevel;

public sealed record UpdateLevelCommand(LevelDto Level) : IRequest<UpdateLevelCommandResponse>;