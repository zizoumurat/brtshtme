using BritishTime.Application.Services.Abstract;
using BritishTime.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace BritishTime.Infrastructure.Services;

public class UserContextService : IUserContextService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<AppUser> _userManager;

    public UserContextService(IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager)
    {
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
    }

    public async Task<AppUser> GetCurrentUserAsync()
    {
        // HTTP context'ten kullanıcıyı al
        var user = _httpContextAccessor.HttpContext?.User;

        if (user == null)
            return null; // Eğer kullanıcı yoksa null döneriz

        // Kullanıcı bilgilerini token'dan alıyoruz (Id, Name vs.)
        var userId = user.FindFirst("Id")?.Value;

        if (string.IsNullOrEmpty(userId))
            return null; // Eğer kullanıcı ID'si boşsa null döneriz

        // Kullanıcıyı veritabanından alıyoruz
        return await _userManager.FindByIdAsync(userId);
    }

    public Task<Guid> GetCurrentUserEmployeeId()
    {
        var user = _httpContextAccessor.HttpContext?.User;

        if (user == null)
            return null; 

        var userId = user.FindFirst("EmployeeId")?.Value;

        return string.IsNullOrEmpty(userId) ? Task.FromResult(Guid.Empty) : Task.FromResult(Guid.Parse(userId)); 
    }

    public async Task<List<string>> GetUserRolesAsync(AppUser user)
    {
        // Kullanıcının rollerini alıyoruz
        return (List<string>)await _userManager.GetRolesAsync(user);
    }
}
