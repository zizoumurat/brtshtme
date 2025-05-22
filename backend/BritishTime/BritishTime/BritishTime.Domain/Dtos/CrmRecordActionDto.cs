using BritishTime.Domain.Enums;

namespace BritishTime.Domain.Dtos;

public class CrmRecordActionDto
{
    public Guid Id { get; set; }
    public Guid CrmRecordId { get; set; }
    public CrmRecordDto CrmRecord { get; set; }
    public Guid EmployeeId { get; set; }
    public string EmployeeName { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public CrmActionType ActionType { get; set; }
    public DateTime? TargetDate { get; set; }
    public string Description { get; set; } = string.Empty;
}

public record AppointmentListDto(Guid CrmRecordId, string Name, string Phone);

public record CrmRecordActionCreateDto
(
    Guid CrmRecordId,
    CrmActionType ActionType,
    DateTime? TargetDate,
    string Description
);

public class CrmRecordActionFilterDto
{
    public Guid? EmployeeId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public CrmActionType? ActionType { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public CrmStatus? Status { get; set; }
    public Guid? DataProviderId { get; set; }
    public Guid? RegionId { get; set; }
}

public class GoogleCalendarResponse
{
    public List<HolidayEvent> Items { get; set; }
}

public class HolidayEvent
{
    public HolidayDate Start { get; set; }
    public string Summary { get; set; }
}

public class HolidayDate
{
    public string Date { get; set; }
}