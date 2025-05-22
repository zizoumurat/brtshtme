using BritishTime.Domain.Abstractions;

namespace BritishTime.Domain.Entities;

public class LessonSession : Entity
{
    public Guid CourseClassId { get; set; }
    public CourseClass CourseClass { get; set; } = null!;
    public DateTime Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public Guid TeacherId { get; set; }
    public Employee Teacher { get; set; } = null!;
}
