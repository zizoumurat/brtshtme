namespace BritishTime.Domain.Dtos;

public sealed record CampaignDto(Guid Id, string Definition, decimal DiscountRate, bool IsActive, Guid BranchId, string BranchName);
public sealed record CampaignCreateDto(string Definition, decimal DiscountRate, bool IsActive, Guid BranchId);
public sealed record CampaignFilterDto(bool? isActive) : SearchDto();