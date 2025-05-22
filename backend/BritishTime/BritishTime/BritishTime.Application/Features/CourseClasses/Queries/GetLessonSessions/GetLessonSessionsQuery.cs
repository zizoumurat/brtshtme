using MediatR;

namespace BritishTime.Application.Features.CourseClasses.Queries.GetLessonSessions;

public sealed record GetLessonSessionsQuery
    (Guid courseClassId) : IRequest<GetLessonSessionsQueryResponse>;
