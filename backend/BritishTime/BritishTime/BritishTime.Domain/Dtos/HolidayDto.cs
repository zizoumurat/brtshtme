namespace BritishTime.Domain.Dtos;

public sealed record HolidayDto(
    Guid Id,
    DateTime Date,
    string Description,
    Guid? BranchId,
    Guid? CourseClassId
);

public sealed record HolidayCreateDto(
    DateTime Date,
    string Description,
    Guid? BranchId,
    Guid? CourseClassId
);