namespace BritishTime.Domain.Dtos;

public record AppUserDto(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string Role,
    string BranchName
);


public sealed record AppUserFilterDto : SearchDto
{
    public string Name { get; init; }
}
