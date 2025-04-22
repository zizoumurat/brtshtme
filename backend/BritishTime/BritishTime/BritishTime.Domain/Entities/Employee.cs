using BritishTime.Domain.Abstractions;
using BritishTime.Domain.Enums;

namespace BritishTime.Domain.Entities;

public class Employee : Entity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string IdentityNumber { get; set; }
    public EmployeeRole Role { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime BirthDate { get; set; }

    public string Phone1 { get; set; }
    public string Phone2 { get; set; }
    public string Phone3 { get; set; }

    public string Email { get; set; }
    public string Address { get; set; }

    public bool IsActive { get; set; } = true;

    // Salary Information
    public SalaryType SalaryType { get; set; } // Enum: Hourly, Monthly, Undefined
    public decimal? SalaryAmount { get; set; }
    public decimal? ExtraPayment { get; set; }
    public string SalaryNote { get; set; }

    // Relation to AppUser
    public Guid? AppUserId { get; set; }
    public AppUser AppUser { get; set; }
}
