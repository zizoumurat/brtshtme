using BritishTime.Domain.Entities;

namespace BritishTime.Application.Services.Abstract;

public interface IUserContextService
{
    Task<AppUser> GetCurrentUserAsync();
    Task<Guid> GetCurrentUserEmployeeId();
    Task<List<string>> GetUserRolesAsync(AppUser user);
}

