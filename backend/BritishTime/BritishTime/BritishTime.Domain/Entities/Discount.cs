using BritishTime.Domain.Abstractions;

namespace BritishTime.Domain.Entities;

public class Discount : Entity
{
    public string Definition { get; set; }
    public decimal DiscountRate { get; set; }
    public bool IsActive { get; set; }

    public Guid BranchId { get; set; }
    public Branch Branch { get; set; } = default!;
}