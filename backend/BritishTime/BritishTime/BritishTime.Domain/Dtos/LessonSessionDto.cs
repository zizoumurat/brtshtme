namespace BritishTime.Domain.Dtos;

public sealed record LessonSessionDto(
    Guid Id,
    DateTime Date,
    TimeOnly StartTime,
    TimeOnly EndTime,
    Guid CourseClassId,
    Guid TeacherId,
    string TeacherFullName
);

public sealed record LessonSessionCreateDto(
    DateTime Date,
    TimeOnly StartTime,
    TimeOnly EndTime,
    Guid CourseClassId,
    Guid TeacherId
);

public sealed record LessonSessionListDto(string Title, string Start, string End);
