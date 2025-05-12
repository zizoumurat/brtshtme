using BritishTime.Domain.Dtos;
using MediatR;

namespace BritishTime.Application.Features.Levels.Commands.CreateLevel;

public sealed record CreateLevelCommand(LevelCreateDto Level) : IRequest<CreateLevelCommandResponse>;