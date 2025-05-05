using BritishTime.Domain.Abstractions;
using BritishTime.Domain.Entities;
using BritishTime.Domain.Enums;

namespace BritishTime.Domain.Dtos;

public class CrmRecordActionDto
{
    public Guid Id { get; set; }
    public Guid CrmRecordId { get; set; }
    public Guid EmployeeId { get; set; }
    public string EmployeeName { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public CrmActionType ActionType { get; set; }
    public DateTime? TargetDate { get; set; }
    public string Description { get; set; } = string.Empty;
}


public record CrmRecordActionCreateDto
(
    Guid CrmRecordId,
    CrmActionType ActionType,
    DateTime? TargetDate,
    string Description
);

public sealed record CrmRecordActionFilterDto() : SearchDto();
