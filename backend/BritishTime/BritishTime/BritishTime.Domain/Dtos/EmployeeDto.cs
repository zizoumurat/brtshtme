using BritishTime.Domain.Enums;

namespace BritishTime.Domain.Dtos;

public record EmployeeDto(
    Guid Id,
    string FirstName,
    string LastName,
    string IdentityNumber,
    EmployeeRole Role,
    DateTime StartDate,
    DateTime BirthDate,
    string Phone1,
    string Phone2,
    string Phone3,
    string Email,
    string Address,
    Guid BranchId,
    bool IsActive,
    SalaryType SalaryType,
    decimal? SalaryAmount,
    decimal? ExtraPayment,
    string SalaryNote,
    string BranchName,
    Guid? AppUserId
);

public record EmployeeCreateDto(
    string FirstName,
    string LastName,
    string IdentityNumber,
    EmployeeRole Role,
    DateTime StartDate,
    DateTime BirthDate,
    string Phone1,
    string Phone2,
    string Phone3,
    string Email,
    string Address,
    Guid BranchId,
    SalaryType SalaryType,
    decimal? SalaryAmount,
    decimal? ExtraPayment,
    string SalaryNote
);

public record AppUserCreateDto(
    Guid EmployeeId,
    List<string> Roles,
    string Password
);

public sealed record EmployeeFilterDto : SearchDto
{
    public string Name { get; init; }
}
