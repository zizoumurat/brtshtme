using BritishTime.Domain.Abstractions;

namespace BritishTime.Domain.Entities;

public class LessonSessionTemplate : Entity
{
    public Guid CourseClassId { get; set; }
    public CourseClass CourseClass { get; set; } = null!;

    public DayOfWeek Day { get; set; }

    public Guid TeacherId { get; set; }
    public Employee Teacher { get; set; } = null!;
}