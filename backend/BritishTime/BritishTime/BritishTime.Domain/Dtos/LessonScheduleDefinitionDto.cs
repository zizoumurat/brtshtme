﻿using BritishTime.Domain.Enums;

namespace BritishTime.Domain.Dtos;

public sealed record LessonScheduleDefinitionDto(
    Guid Id,
    ScheduleType StudentType,
    EducationType EducationType,
    Guid BranchId,
    string BranchName,
    string Schedule,
    string ScheduleCode,
    int DayCount,
    int DayHour,
    List<DayOfWeek> Days,
    TimeOnly StartTime,
    TimeOnly EndTime,
    ScheduleCategory ScheduleCategory,
    decimal Discount
);

public sealed record LessonScheduleDefinitionCreateDto(
    Guid? Id,
    ScheduleType StudentType,
    EducationType EducationType,
    string Schedule,
    string ScheduleCode,
    int DayHour,
    List<DayOfWeek> Days,
    string StartTime,
    ScheduleCategory ScheduleCategory,
    Guid BranchId,
    decimal Discount
);

public sealed record LessonScheduleDefinitionFilterDto(
    ScheduleType? StudentType,
    EducationType? EducationType,
    ScheduleCategory? ScheduleCategory
) : SearchDto;
