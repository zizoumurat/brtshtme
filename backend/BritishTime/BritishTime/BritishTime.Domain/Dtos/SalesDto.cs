using BritishTime.Domain.Enums;

namespace BritishTime.Domain.Dtos;

public sealed record SalesDto(Guid Id, string Name, int Capacity, Guid BranchId, string BranchName);
public sealed record SalesCreateDto(string Name, int Capacity, Guid BranchId);
public sealed record SalesFilterDto() : SearchDto();

public sealed record CalculatePaymentDto(
    Guid LessonScheduleId,
    Guid BranchId,
    int LevelCount,
    int? InstallmentCount,
    PaymentMethod PaymentMethod,
    Guid? CampaignId,
    Guid? DiscountId,
    decimal? DownPayment,              
    DateTime? FirstInstallmentDate     
);

public sealed record InstallmentDto(DateTime DueDate, decimal Amount);

public sealed record CalculatePaymentResultDto(
    decimal TotalAmount,
    decimal FinancedAmount,
    List<InstallmentDto> Installments
);
