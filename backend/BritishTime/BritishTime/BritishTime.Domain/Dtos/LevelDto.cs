namespace BritishTime.Domain.Dtos;

public sealed record LevelDto(Guid Id, string Name, string Definition);
public sealed record LevelCreateDto(string Name, string Definition);
public sealed record LevelFilterDto() : SearchDto();