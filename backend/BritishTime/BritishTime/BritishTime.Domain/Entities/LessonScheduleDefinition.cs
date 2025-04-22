using BritishTime.Domain.Abstractions;
using BritishTime.Domain.Enums;

namespace BritishTime.Domain.Entities;

public class LessonScheduleDefinition : Entity
{
    public StudentType StudentType { get; set; }
    public EducationType EducationType { get; set; }

    public string Schedule { get; set; } = null!; 
    public string ScheduleCode { get; set; } = null!;

    public int DayCount { get; set; } 
    public int DayHour { get; set; } 
    public List<DayOfWeek> Days { get; set; } = new();

    public TimeOnly StartTime { get; set; } 
    public TimeOnly EndTime { get; set; }  

    public decimal Discount { get; set; }

    public ScheduleCategory ScheduleCategory { get; set; }

    public Guid BranchId { get; set; }
    public Branch Branch { get; set; } = default!;
}
