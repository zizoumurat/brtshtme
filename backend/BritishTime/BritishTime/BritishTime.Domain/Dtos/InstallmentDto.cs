namespace BritishTime.Domain.Dtos;

public sealed record InstallmentDto(
    Guid Id,
    DateTime DueDate,
    decimal Amount,
    Guid ContractId
);

public sealed record InstallmentCreateDto(
    DateTime DueDate,
    decimal Amount,
    Guid ContractId
);

public sealed record InstallmentListDto(DateTime DueDate, decimal Amount, bool IsDeposit = false);

public sealed record InstallmentFilterDto : SearchDto
{
    public Guid? ContractId { get; init; }
    public DateTime? FromDate { get; init; }
    public DateTime? ToDate { get; init; }
}