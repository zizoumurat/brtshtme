using BritishTime.Domain.Enums;

namespace BritishTime.Domain.Dtos;

public sealed record LessonScheduleDefinitionDto(
    Guid Id,
    StudentType StudentType,
    EducationType EducationType,
    string ScheduleCode,
    int DayCount,
    int DayHour,
    List<DayOfWeek> Days,
    TimeOnly StartTime,
    TimeOnly EndTime,
    ScheduleCategory ScheduleCategory
);

public sealed record LessonScheduleDefinitionCreateDto(
    StudentType StudentType,
    EducationType EducationType,
    string ScheduleCode,
    int DayHour,
    List<DayOfWeek> Days,
    TimeOnly StartTime,
    ScheduleCategory ScheduleCategory,
    Guid BranchId
);

public sealed record LessonScheduleDefinitionFilterDto(
    StudentType? StudentType,
    EducationType? EducationType,
    ScheduleCategory? ScheduleCategory
) : SearchDto;
