using BritishTime.Domain.Enums;

namespace BritishTime.Domain.Dtos;

public sealed record IncentiveSettingDto(
    Guid Id,
    ParticipantType ParticipantType,
    decimal MinAmount,
    decimal MaxAmount,
    decimal SalesCommission,
    decimal CollectionCommission,
    decimal Bonus,
    string Note
);
public sealed record IncentiveSettingCreateDto(
    ParticipantType ParticipantType,
    decimal MinAmount,
    decimal MaxAmount,
    decimal SalesCommission,
    decimal CollectionCommission,
    decimal Bonus,
    string Note
);
public sealed record IncentiveSettingFilterDto(ParticipantType ParticipantType) : SearchDto();
