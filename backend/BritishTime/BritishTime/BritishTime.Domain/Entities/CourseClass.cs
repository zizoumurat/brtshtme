using BritishTime.Domain.Abstractions;
using BritishTime.Domain.Enums;

namespace BritishTime.Domain.Entities;

public class CourseClass : Entity
{
    public Guid BranchId { get; set; }
    public Branch Branch { get; set; } = null!;

    public string Name { get; set; }

    public ClassType ClassType { get; set; }

    public EducationType EducationType { get; set; }

    public ScheduleType ScheduleType { get; set; }

    public Guid LessonScheduleDefinitionId { get; set; }

    public Guid LevelId { get; set; }

    public string Description { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int Capacity { get; set; }

    public int Unit { get; set; }

    public Guid? ClassroomId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
