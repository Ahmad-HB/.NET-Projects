using System.Security.Claims;
using Domain.Constants;
using Domain.Entity;
using Infrastructure.ViewModel;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Seeds;

public static class DefultUser
{
    public static async Task SeedBasicUserAsync(UserManager<ApplicationUser> userManager,  RoleManager<IdentityRole> roleManager)
    {
        var DefultUser = new ApplicationUser
        {
            UserName = Helper.UserNameBasic,
            Email = Helper.EmailBasic,
            Name = Helper.NameBasic,
            ImageUser ="user1.jpg",
            ActiveUser = true,
            EmailConfirmed = true
        };

        var user = userManager.FindByEmailAsync(DefultUser.Email);
        if (user.Result == null)
        {
            await userManager.CreateAsync(DefultUser, Helper.Password);
            await userManager.AddToRolesAsync(DefultUser,
                new List<string>
                {
                    Helper.Roles.Basic.ToString()
                });
        }
    }
    
    
    public static async Task SeedSuperAdminAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        var DefultUser = new ApplicationUser
        {
            UserName = Helper.UserName,
            Email = Helper.Email,
            Name = Helper.Name,
            ImageUser ="user1.jpg",
            ActiveUser = true,
            EmailConfirmed = true
        };

        var user =await userManager.FindByEmailAsync(DefultUser.Email);
        if (user == null)
        {
            await userManager.CreateAsync(DefultUser, Helper.Password);
            await userManager.AddToRolesAsync(DefultUser,
                new List<string>
                {
                    Helper.Roles.SuperAdmin.ToString()
                });
        }

        await roleManager.seedClamisAsync();

    }


    public static async Task seedClamisAsync(this RoleManager<IdentityRole> roleManager)
    {
        var adminRole = await roleManager.FindByNameAsync(Helper.Roles.SuperAdmin.ToString());
        var moduels = Enum.GetValues(typeof(Helper.PermissionModuleName));
        foreach (var module in moduels)
        {
            await roleManager.AddPermissionsClaim(adminRole, module.ToString());
        }

        //code add 
    }

    public static async Task AddPermissionsClaim(this RoleManager<IdentityRole> roleManager,IdentityRole role, string moudle)
    {
        var allClaims = await roleManager.GetClaimsAsync(role);
        var allPermissions = Permissions.GeneratePermissionsFromModule(moudle);

        foreach (var permissions in allPermissions)
        {
            if (!allClaims.Any(x=> x.Type== Helper.Permission && x.Value == permissions))
            {
                await roleManager.AddClaimAsync(role,new Claim(Helper.Permission, permissions));
            }
        }
    }
}