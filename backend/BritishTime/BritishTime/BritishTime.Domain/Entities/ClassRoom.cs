using BritishTime.Domain.Abstractions;

namespace BritishTime.Domain.Entities;

public class ClassRoom : Entity
{
    public string Name { get; set; }
    public int Capacity { get; set; }

    public Guid BranchId { get; set; }
    public Branch Branch { get; set; } = default!;
}