using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;
using MediatR;


namespace BritishTime.Application.Features.LessonScheduleDefinitionsFeatures.Queries.GetAllLessonScheduleDefinitions;

public sealed record GetAllLessonScheduleDefinitionsQuery
    (LessonScheduleDefinitionFilterDto filter, PageRequest pagination) : IRequest<GetAllLessonScheduleDefinitionsQueryResponse>;
