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

            var systemRoles = new[] { "Admin", "CRM", "SRM", "Muhasebe" };

            foreach (var item in systemRoles)
            {
                if (!roleManager.RoleExistsAsync(item).Result)
                {
                    AppRole role = new()
                    {
                        Name = item,
                        NormalizedName = item.ToUpper()
                    };
                    roleManager.CreateAsync(role).Wait();
                }

            }

            if (!userManager.Users.Any(p => p.UserName == "admin"))
            {

                AppUser user = new()
                {
                    UserName = "admin",
                    Email = "admin@admin.com",
                    FirstName = "Murat",
                    LastName = "Dere",
                    BranchId = Guid.Parse("c3740f2d-2ef8-4d45-a7df-d1a5bb1577c6"),
                    EmailConfirmed = true,
                };

                userManager.CreateAsync(user, "Zizou!5#5").Wait();
                userManager.AddToRoleAsync(user, "Admin").Wait();
            }
        }
    }

}
