using BritishTime.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace BritishTime.Domain.Dtos;

public sealed record StudentDto(
    Guid Id,
    string FirstName,
    string LastName,
    string IdentityNumber,
    DateTime BirthDate,
    string Phone,
    string SecondPhone,
    string Email,
    StudentType StudentType,
    string Address,
    int CityId,
    int DistrictId,
    string ParentFirstName,
    string ParentLastName,
    string ParentPhone,
    string ParentIdentityNumber,
    DateTime? ParentBirthDate,
    Guid BranchId,
    Guid? CrmRecordId,
    Guid UserId
);

public class StudentCreateDto
{
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public string IdentityNumber { get; set; }

    [Required]
    public DateTime BirthDate { get; set; }

    public string Phone { get; set; }
    public string SecondPhone { get; set; }
    public string Email { get; set; }

    [Required]
    public StudentType StudentType { get; set; }

    [Required]
    public string Address { get; set; }

    [Required]
    public int CityId { get; set; }

    [Required]
    public int DistrictId { get; set; }

    public string ParentFirstName { get; set; }
    public string ParentLastName { get; set; }
    public string ParentPhone { get; set; }
    public string ParentIdentityNumber { get; set; }
    public DateTime? ParentBirthDate { get; set; }

    [Required]
    public Guid BranchId { get; set; }

    public Guid? CrmRecordId { get; set; }
}

public sealed record StudentFilterDto : SearchDto
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Phone { get; init; }
    public string Email { get; init; }
    public StudentType? StudentType { get; init; }
    public Guid? BranchId { get; init; }
}

public sealed class CreateStudentRequest
{
    public StudentCreateDto Student { get; set; }
    public CalculatePaymentDto Payment { get; set; }
}