﻿using BritishTime.Domain.Dtos;
using MediatR;

namespace BritishTime.Application.Features.LessonScheduleDefinitions.Commands.UpdateLessonScheduleDefinition;

public sealed record UpdateLessonScheduleDefinitionCommand(LessonScheduleDefinitionDto LessonScheduleDefinition) : IRequest<UpdateLessonScheduleDefinitionCommandResponse>;