namespace BritishTime.Domain.Dtos;

public sealed record CourseSaleSettingDto(Guid Id, int MinLevel, int MaxLevel, decimal Rate, Guid BranchId, string BranchName);
public sealed record CourseSaleSettingCreateDto(int MinLevel, int MaxLevel, decimal Rate, Guid BranchId);
public sealed record CourseSaleSettingFilterDto() : SearchDto();