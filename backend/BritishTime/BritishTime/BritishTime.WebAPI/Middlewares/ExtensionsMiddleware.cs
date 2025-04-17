using BritishTime.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace BritishTime.WebAPI.Middlewares;

public static class ExtensionsMiddleware
{
    public static void CreateFirstUser(WebApplication app)
    {
        using (var scoped = app.Services.CreateScope())
        {
            var userManager = scoped.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            var roleManager = scoped.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();

            // Admin rolü var mı kontrol et
            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                AppRole role = new()
                {
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper()
                };
                roleManager.CreateAsync(role).Wait();
            }

            // Kullanıcı var mı kontrol et
            if (!userManager.Users.Any(p => p.UserName == "admin"))
            {
                AppUser user = new()
                {
                    UserName = "admin",
                    Email = "admin@admin.com",
                    FirstName = "Murat",
                    LastName = "Dere",
                    BranchId = Guid.Parse("c3740f2d-2ef8-4d45-a7df-d1a5bb1577c6"),
                    EmailConfirmed = true
                };

                userManager.CreateAsync(user, "Zizou!5#5").Wait();

                // Admin rolünü kullanıcıya ata
                userManager.AddToRoleAsync(user, "Admin").Wait();
            }

            // İstersen başka roller de eklenebilir:
            if (!roleManager.RoleExistsAsync("User").Result)
            {
                AppRole userRole = new()
                {
                    Name = "User",
                    NormalizedName = "User".ToUpper()
                };
                roleManager.CreateAsync(userRole).Wait();
            }
        }
    }

}
