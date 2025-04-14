namespace BritishTime.Domain.Dtos;

public sealed record RegionDto(Guid Id, string Name, string Description);
public sealed record RegionCreateDto(string Name, string Description);
public sealed record RegionFilterDto() : SearchDto();