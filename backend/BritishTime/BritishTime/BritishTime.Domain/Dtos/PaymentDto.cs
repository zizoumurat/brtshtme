namespace BritishTime.Domain.Dtos;

public sealed record PaymentDto(Guid Id, string Name, string Definition);
public sealed record PaymentCreateDto(string Name, string Definition);
public sealed record PaymentFilterDto() : SearchDto();  