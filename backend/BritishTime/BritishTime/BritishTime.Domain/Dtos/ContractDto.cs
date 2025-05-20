using BritishTime.Domain.Enums;

namespace BritishTime.Domain.Dtos;

public sealed record ContractDto(
    Guid Id,
    Guid StudentId,
    string StudentFullName,
    DateTime StartDate,
    int LevelCount,
    int UsedLevelCount,
    decimal TotalAmount,
    PaymentMethod PaymentMethod,
    int? InstallmentCount,
    decimal? Deposit,
    DateTime FirstInstallmentDate,
    ContractType ContractType,
    EducationType EducationType,
    Signatory Signatory,
    Guid LessonScheduleId,
    string LessonScheduleName,
    Guid? CampaignId,
    Guid? DiscountId
);

public sealed record ContractCreateDto(
    Guid StudentId,
    DateTime StartDate,
    int LevelCount,
    decimal TotalAmount,
    PaymentMethod PaymentMethod,
    int? InstallmentCount,
    decimal? Deposit,
    DateTime FirstInstallmentDate,
    ContractType ContractType,
    EducationType EducationType,
    Signatory Signatory,
    Guid LessonScheduleId,
    Guid? CampaignId,
    Guid? DiscountId
);

public sealed record ContractFilterDto : SearchDto
{
    public Guid? StudentId { get; init; }
    public ContractType? ContractType { get; init; }
    public EducationType? EducationType { get; init; }
    public Guid? BranchId { get; init; }
}
