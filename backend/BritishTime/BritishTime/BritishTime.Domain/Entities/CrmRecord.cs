using BritishTime.Domain.Abstractions;
using BritishTime.Domain.Enums;

namespace BritishTime.Domain.Entities;

public class CrmRecord : Entity
{
    public string Phone { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string SecondPhone { get; set; }
    public string Email { get; set; }

    public Guid DataProviderId { get; set; } 
    public Employee DataProvider { get; set; }

    public Guid SalesRepresentativeId { get; set; }
    public Employee SalesRepresentative { get; set; }

    public Guid RegionId { get; set; }
    public Region Region { get; set; }

    public Guid BranchId { get; set; } 
    public Branch Branch { get; set; }

    public CrmDataSource DataSource { get; set; }
    public bool ExcludeFromCommission { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public CrmStatus Status { get; set; }
    public ICollection<CrmRecordAction> Actions { get; set; } = new List<CrmRecordAction>();
}
