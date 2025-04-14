﻿
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;

namespace BritishTime.Application.Features.LessonScheduleDefinitionsFeatures.Queries.GetAllLessonScheduleDefinitions;
public sealed record GetAllLessonScheduleDefinitionsQueryResponse(PaginatedList<LessonScheduleDefinitionDto> result);
