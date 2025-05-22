using MediatR;

namespace BritishTime.Application.Features.CourseClasses.Queries.GetLessonSessionsByTeacher;

public sealed record GetLessonSessionsByTeacherQuery
    (Guid employeeId) : IRequest<GetLessonSessionsByTeacherQueryResponse>;
