using BritishTime.Domain.Abstractions;

namespace BritishTime.Domain.Entities;

public class Level : Entity
{
    public string Name { get; set; }
    public string Definition { get; set; }
}
