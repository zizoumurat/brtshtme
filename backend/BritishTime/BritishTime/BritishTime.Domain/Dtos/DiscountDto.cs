namespace BritishTime.Domain.Dtos;

public sealed record DiscountDto(Guid Id, string Definition, decimal DiscountRate, bool IsActive, Guid BranchId, string BranchName);
public sealed record DiscountCreateDto(string Definition, decimal DiscountRate, bool IsActive, Guid BranchId);
public sealed record DiscountFilterDto() : SearchDto();