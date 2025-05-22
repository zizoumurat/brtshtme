namespace BritishTime.Domain.Dtos;

public sealed record LessonSessionTemplateDto(
    Guid Id,
    DayOfWeek Day,
    Guid CourseClassId,
    Guid TeacherId
);
public sealed record LessonSessionTemplateCreateDto(
    DayOfWeek Day,
    Guid CourseClassId,
    Guid TeacherId
);
