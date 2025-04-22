namespace BritishTime.Domain.Dtos;

public sealed record InstallmentSettingDto(Guid Id, int Level, int MaxBond, int MaxCardInstallment, Guid BranchId, string BranchName);
public sealed record InstallmentSettingCreateDto(int Level, int MaxBond, int MaxCardInstallment, Guid BranchId);
public sealed record InstallmentSettingFilterDto() : SearchDto();