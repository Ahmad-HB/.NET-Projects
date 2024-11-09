using Domain.Entity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Seeds;

public static class DefultRole
{
    public static async Task SeedAsync(RoleManager<IdentityRole> roleManager)
    {
        // if (!roleManager.Roles.Any())
        // {
            await roleManager.CreateAsync(new IdentityRole(Helper.Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Helper.Roles.SuperAdmin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Helper.Roles.Basic.ToString()));
        // }
    }
}