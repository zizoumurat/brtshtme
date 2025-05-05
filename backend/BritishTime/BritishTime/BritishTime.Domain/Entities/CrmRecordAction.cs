using BritishTime.Domain.Abstractions;
using BritishTime.Domain.Enums;

namespace BritishTime.Domain.Entities;

public class CrmRecordAction : Entity
{
    public Guid CrmRecordId { get; set; }
    public CrmRecord CrmRecord { get; set; }

    public Guid EmployeeId { get; set; }
    public Employee Employee { get; set; }

    public CrmActionType ActionType { get; set; }

    public DateTime? TargetDate { get; set; } 

    public string Description { get; set; }
    public DateTime Date { get; set; }
}
