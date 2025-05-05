using BritishTime.Domain.Enums;

namespace BritishTime.Domain.Dtos;

public record CrmRecordDto(
    Guid Id,
    string Phone,
    string FirstName,
    string LastName,
    string SecondPhone,
    string Email,
    Guid DataProviderId,
    Guid SalesRepresentativeId,
    Guid RegionId,
    CrmDataSource DataSource,
    bool ExcludeFromCommission,
    string Description,
    DateTime Date,
    CrmStatus Status,
    Guid BranchId
);

public record CrmRecordCreateDto(
    string Phone,
    string FirstName,
    string LastName,
    string SecondPhone,
    string Email,
    Guid DataProviderId,
    Guid RegionId,
    CrmDataSource DataSource,
    bool ExcludeFromCommission,
    string Description,
    Guid BranchId
);

public sealed record CrmRecordFilterDto() : SearchDto();