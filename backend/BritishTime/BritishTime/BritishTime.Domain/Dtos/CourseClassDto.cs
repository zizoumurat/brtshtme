using BritishTime.Domain.Enums;

namespace BritishTime.Domain.Dtos;

public sealed record CourseClassDto(
    Guid Id,
    Guid BranchId,
    BranchDto Branch,
    string Name,
    ClassType ClassType,
    EducationType EducationType,
    ScheduleType ScheduleType,
    Guid LessonScheduleDefinitionId,
    Guid LevelId,
    string Description,
    DateTime StartDate,
    DateTime EndDate,
    int Capacity,
    int Unit,
    string Note,
    Guid? ClassroomId
);

public sealed record CourseClassCreateDto(
    Guid BranchId,
    string Name,
    ClassType ClassType,
    EducationType EducationType,
    ScheduleType ScheduleType,
    Guid LessonScheduleDefinitionId,
    Guid LevelId,
    string Description,
    DateTime StartDate,
    DateTime EndDate,
    int Capacity,
    int Unit,
    string Note,
    Guid? ClassroomId
);

public sealed record CourseClassFilterDto(
    Guid? BranchId = null,
    string Name = null,
    ClassType? ClassType = null,
    EducationType? EducationType = null,
    ScheduleType? ScheduleType = null,
    Guid? LevelId = null,
    DateTime? StartDateFrom = null,
    DateTime? StartDateTo = null,
    int? Capacity = null,
    int? Unit = null
) : SearchDto;

public sealed record CourseEndDateRequest(DateTime StartDate,
    Guid LessonScheduleId
);

public sealed record CreateLessonSessionDto(Guid classId, Dictionary<DayOfWeek, Guid> programDaysWithTeachers);
