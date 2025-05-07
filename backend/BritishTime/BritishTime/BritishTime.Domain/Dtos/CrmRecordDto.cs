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
    string RegionName,
    string DataProviderFirstName,
    string DataProviderLastName,
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

public class CrmRecordFilterDto
{
    public Guid? EmployeeId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public CrmStatus? Status { get; set; }
    public Guid? DataProviderId { get; set; }
    public Guid? RegionId { get; set; }
}