using BritishTime.Domain.Abstractions;

namespace BritishTime.Domain.Entities;

public sealed class Branch : Entity
{
    public string Name { get; set; } = null!;
    public string Description { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public int LessonDurationInMinutes { get; set; }
    public int BreakDurationInMinutes { get; set; } 
    public int LevelDurationInHours { get; set; } 
    public ICollection<AppUser> Users { get; set; } = new List<AppUser>();

    public ICollection<LessonScheduleDefinition> LessonScheduleDefinitions { get; set; } = new List<LessonScheduleDefinition>();
}
