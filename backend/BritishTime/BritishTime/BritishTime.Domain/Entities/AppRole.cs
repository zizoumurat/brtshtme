using Microsoft.AspNetCore.Identity;

namespace BritishTime.Domain.Entities;

public sealed class AppRole : IdentityRole<Guid>
{
    public string Description { get; set; }
}
