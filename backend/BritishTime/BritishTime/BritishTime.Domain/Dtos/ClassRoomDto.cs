namespace BritishTime.Domain.Dtos;

public sealed record ClassRoomDto(Guid Id, string Name, int Capacity, Guid BranchId, string BranchName);
public sealed record ClassRoomCreateDto(string Name, int Capacity, Guid BranchId);
public sealed record ClassRoomFilterDto() : SearchDto();