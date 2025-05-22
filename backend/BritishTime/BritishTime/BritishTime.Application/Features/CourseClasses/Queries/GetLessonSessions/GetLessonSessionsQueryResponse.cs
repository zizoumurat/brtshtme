using BritishTime.Domain.Dtos;

namespace BritishTime.Application.Features.CourseClasses.Queries.GetLessonSessions;

public sealed record GetLessonSessionsQueryResponse(List<LessonSessionListDto> result);
