using BritishTime.Domain.Abstractions;
using BritishTime.Domain.Enums;

namespace BritishTime.Domain.Entities;

public sealed class IncentiveSetting : Entity
{
    public ParticipantType ParticipantType { get; set; }
    public decimal MinAmount { get; set; }
    public decimal MaxAmount { get; set; }
    public decimal SalesCommission { get; set; }
    public decimal CollectionCommission { get; set; }
    public decimal Bonus { get; set; }
    public string Note { get; set; }
}
