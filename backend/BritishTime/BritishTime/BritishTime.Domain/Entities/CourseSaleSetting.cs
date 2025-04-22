using BritishTime.Domain.Abstractions;

namespace BritishTime.Domain.Entities;

public class CourseSaleSetting : Entity
{
    public int MinLevel { get; set; }
    public int MaxLevel { get; set; }
    public decimal Rate { get; set; }

    public Guid BranchId { get; set; }
    public Branch Branch { get; set; } = default!;
}
