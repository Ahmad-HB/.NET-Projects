using System.Security.Claims;
using Domain.Constants;
using Domain.Entity;
using Infrastructure.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FreeBooks.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize]
public class PermissionsController : Controller
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public PermissionsController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    // GET: Display Permissions
    public async Task<IActionResult> Permission(string roleId)
    {
        if (string.IsNullOrEmpty(roleId))
        {
            return BadRequest("Role ID cannot be null or empty.");
        }

        var role = await _roleManager.FindByIdAsync(roleId);
        if (role == null)
        {
            return NotFound($"Role with ID '{roleId}' not found.");
        }

        var claims = await _roleManager.GetClaimsAsync(role);
        var claimValues = claims.Select(x => x.Value).ToList();
        
        var allPermissions = Permissions.PermissionList()
            .Select(x => new RoleClaimViewModel { value = x })
            .ToList();

        foreach (var permission in allPermissions)
        {
            if (claimValues.Contains(permission.value))
            {
                permission.selected = true;
            }
        }

        var model = new PermissionViewModel
        {
            RoleId = roleId,
            RoleName = role.Name,
            RoleClaims = allPermissions
        };

        return View(model);
    }

    // POST: Update Permissions
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(PermissionViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View("Permission", model); // Return the model with validation errors
        }

        var role = await _roleManager.FindByIdAsync(model.RoleId);
        if (role == null)
        {
            return NotFound($"Role with ID '{model.RoleId}' not found.");
        }

        var claims = await _roleManager.GetClaimsAsync(role);
        foreach (var claim in claims)
        {
            await _roleManager.RemoveClaimAsync(role, claim);
        }

        var selectedClaims = model.RoleClaims.Where(x => x.selected).ToList();
        foreach (var claim in selectedClaims)
        {
            await _roleManager.AddClaimAsync(role, new Claim(Helper.Permission, claim.value));
        }

        return RedirectToAction("Roles","Accounts");
    }
}
