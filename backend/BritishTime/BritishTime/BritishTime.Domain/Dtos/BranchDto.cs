namespace BritishTime.Domain.Dtos;

public sealed record BranchDto(Guid Id, string Name, string Description, string Address, string PhoneNumber, string Email, int LessonDurationInMinutes, int BreakDurationInMinutes, int LevelDurationInHours);
public sealed record BranchCreateDto(string Name, string Description, string Address, string PhoneNumber, string Email, int LessonDurationInMinutes, int BreakDurationInMinutes, int LevelDurationInHours);

public sealed record BranchFilterDto : SearchDto
{
    public string Name { get; init; }
    public string Address { get; init; }
}