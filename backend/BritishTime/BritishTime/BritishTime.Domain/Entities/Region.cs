using BritishTime.Domain.Abstractions;

namespace BritishTime.Domain.Entities;

public sealed class Region : Entity
{
    public string Name { get; set; } = null!;
    public string Description { get; set; }
}
