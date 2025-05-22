using BritishTime.Domain.Dtos;

namespace BritishTime.Application.Features.CourseClasses.Queries.GetLessonSessionsByTeacher;

public sealed record GetLessonSessionsByTeacherQueryResponse(List<LessonSessionListDto> result);
