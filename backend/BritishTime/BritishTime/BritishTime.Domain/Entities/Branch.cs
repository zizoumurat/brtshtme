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
    public ICollection<Campaign> Campaigns { get; set; } = new List<Campaign>();
    public ICollection<ClassRoom> ClassRooms { get; set; } = new List<ClassRoom>();
    public ICollection<CourseSaleSetting> CourseSaleSettings { get; set; } = new List<CourseSaleSetting>();
    public ICollection<Discount> Discounts { get; set; } = new List<Discount>();
    public ICollection<Employee> Employees { get; set; }

    public ICollection<InstallmentSetting> InstallmentSettings { get; set; } = new List<InstallmentSetting>();
    public ICollection<LessonScheduleDefinition> LessonScheduleDefinitions { get; set; } = new List<LessonScheduleDefinition>();
    public ICollection<BranchPricingSetting> BranchPricingSettings { get; set; }
    public ICollection<CourseClass> CourseClasses { get; set; }
}
