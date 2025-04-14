using BritishTime.Domain.Dtos;
using MediatR;

namespace BritishTime.Application.Features.LessonScheduleDefinitions.Commands.DeleteLessonScheduleDefinition;

public sealed record DeleteLessonScheduleDefinitionCommand(Guid Id) : IRequest<DeleteLessonScheduleDefinitionCommandResponse>;