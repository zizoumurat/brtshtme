using BritishTime.Domain.Abstractions;
using BritishTime.Domain.Enums;

namespace BritishTime.Domain.Entities;

public class Student : Entity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string IdentityNumber { get; set; }
    public DateTime BirthDate { get; set; }
    public string Phone { get; set; }
    public string SecondPhone { get; set; }
    public string Email { get; set; }
    public StudentType StudentType { get; set; }
    public string Address { get; set; }
    public int CityId { get; set; }
    public int DistrictId { get; set; }

    // Ebeveyn bilgileri (opsiyonel)
    public string ParentFirstName { get; set; }
    public string ParentLastName { get; set; }
    public string ParentPhone { get; set; }
    public string ParentIdentityNumber { get; set; }
    public DateTime? ParentBirthDate { get; set; }

    public Guid BranchId { get; set; }
    public Branch Branch { get; set; }

    public Guid? CrmRecordId { get; set; }
    public CrmRecord CrmRecord { get; set; }

    public Guid UserId { get; set; }
    public AppUser User { get; set; }

    public ICollection<Contract> Contracts { get; set; } = [];
    public ICollection<Payment> Payments { get; set; } = [];
}

