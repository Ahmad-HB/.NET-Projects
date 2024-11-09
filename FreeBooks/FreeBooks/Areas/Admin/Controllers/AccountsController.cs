using Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Data;
using Infrastructure.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Domain.Constants;
using Microsoft.AspNetCore.Authorization;


namespace FreeBooks.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Permissions.Accounts.View)]
public class AccountsController : Controller
{
    #region Declaration

    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly FreeBookDbContext _context;

    #endregion


    #region Constructor

    public AccountsController(RoleManager<IdentityRole> roleManager,
        UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
        FreeBookDbContext context)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;
    }

    #endregion


    #region Methods
    
    [Authorize(Permissions.Roles.View)]
    public IActionResult Roles()
    {
        return View(new RolesViewModel
        {
            NewRole = new NewRole(),
            Roles = _roleManager.Roles.OrderBy(x => x.Name).ToList()
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Permissions.Roles.Create)]
    public async Task<IActionResult> Roles(RolesViewModel model)
    {
        if (true)
        {
            if (model.NewRole.RoleId == Guid.Empty) // Create new role
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(model.NewRole.RoleName));
                if (result.Succeeded)
                {
                    // TempData["SuccessMessage"] = "Role created successfully.";
                    HttpContext.Session.SetString("msgType", "success");
                    HttpContext.Session.SetString("title", Resource.Resources.lbSave);
                    HttpContext.Session.SetString("msg", Resource.Resources.lbSaveMsgRole);
                    return RedirectToAction("Roles");
                }
                else
                {
                    // TempData["ErrorMessage"] = "Error creating role.";
                    HttpContext.Session.SetString("msgType", "error");
                    HttpContext.Session.SetString("title", Resource.Resources.lbNotSaved);
                    HttpContext.Session.SetString("msg", Resource.Resources.lbNotSavedMsgRole);
                    return RedirectToAction("Roles");
                }
            }
            else // Update existing role
            {
                var role = await _roleManager.FindByIdAsync(model.NewRole.RoleId.ToString());
                if (role != null)
                {
                    role.Name = model.NewRole.RoleName;
                    var result = await _roleManager.UpdateAsync(role);
                    if (result.Succeeded)
                    {
                        // TempData["SuccessMessage"] = "Role updated successfully.";
                        HttpContext.Session.SetString("msgType", "success");
                        HttpContext.Session.SetString("title", Resource.Resources.lbUpdate);
                        HttpContext.Session.SetString("msg", Resource.Resources.lbUpdateMsgRole);
                    }
                    else
                    {
                        // TempData["ErrorMessage"] = "Error updating role.";
                        HttpContext.Session.SetString("msgType", "error");
                        HttpContext.Session.SetString("title", Resource.Resources.lbNotUpdate);
                        HttpContext.Session.SetString("msg", Resource.Resources.lbNotUpdateMsgRole);
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "Role not found.";
                }
            }

            return RedirectToAction(nameof(Roles));
        }

        // If we got this far, something failed, redisplay form with the current roles
        model.Roles = _roleManager.Roles.OrderBy(x => x.Name).ToList();
        return View(model);
    }

    // [HttpPost]
    // [ValidateAntiForgeryToken]
    // public async Task<IActionResult> Roles(RolesViewModel model)
    // {
    //     if (true)
    //     {
    //        
    //         if (model.NewRole.RoleId == Guid.Empty)
    //         {
    //             var Result = await _roleManager.CreateAsync(new IdentityRole(model.NewRole.RoleName));
    //             if (Result.Succeeded)
    //             {
    //                 HttpContext.Session.SetString("msgType", "success");
    //                 HttpContext.Session.SetString("title", Resource.Resources.lbSave);
    //                 HttpContext.Session.SetString("msg", "user saved successfully");
    //                 return RedirectToAction("Roles");
    //             }
    //             else
    //             {
    //                 HttpContext.Session.SetString("msgType", "error");
    //                 HttpContext.Session.SetString("title", "save not done");
    //                 HttpContext.Session.SetString("msg", "user did not save successfully");
    //                 HttpContext.Session.SetString("msg", "user saved successfully");
    //                 return RedirectToAction("Roles");
    //             }
    //         }
    //         else
    //         {
    //             var RoleUpdate = await _roleManager.FindByIdAsync(model.NewRole.RoleId.ToString());
    //             RoleUpdate.Id = model.NewRole.RoleId.ToString();
    //             RoleUpdate.Id = model.NewRole.RoleName;
    //             var result = await _roleManager.UpdateAsync(RoleUpdate);
    //             if (result.Succeeded)
    //             {
    //                 HttpContext.Session.SetString("msgType", "success");
    //                 HttpContext.Session.SetString("title", Resource.Resources.lbUpdate);
    //                 HttpContext.Session.SetString("msg", "user Eidt successfully");
    //             }
    //             else
    //             {
    //                 HttpContext.Session.SetString("msgType", "error");
    //                 HttpContext.Session.SetString("title", "Eidt not done");
    //                 HttpContext.Session.SetString("msg", "user did not Eidt successfully");
    //             }
    //             
    //         }
    //     }
    //     return View();
    // }
    
    
    [Authorize(Permissions.Roles.Delete)]
    public async Task<IActionResult> DeleteRole(string Id)
    {
        var role = _roleManager.Roles.FirstOrDefault(x => x.Id == Id);
        if ((await _roleManager.DeleteAsync(role)).Succeeded)
        {
            return RedirectToAction(nameof(Roles));
        }

        return RedirectToAction("Roles");
    }

    [Authorize(Permissions.Accounts.View)]
    public IActionResult Register()
    {
        return View(new RegisterViewModel
        {
            NewRegister = new NewRegister(),
            Roles = _roleManager.Roles.OrderBy(x => x.Name).ToList(),
            Users = _context.VwUsers.OrderBy((x => x.Role)).ToList() //_userManager.Users.OrderBy(x => x.Name).ToList()
        });
    }


    //     [HttpPost]
//     [ValidateAntiForgeryToken]
//     public async Task<IActionResult> Register(RegisterViewModel model)
// {
//     if (false)
//     {
//         return View(model); // return the same view with validation errors
//     }
//
//     var user = new ApplicationUser
//     {
//         Id = model.NewRegister.Id.ToString(),
//         Name = model.NewRegister.Name,
//         UserName = model.NewRegister.Email,
//         Email = model.NewRegister.Email,
//         ActiveUser = model.NewRegister.ActiveUser,
//         ImageUser = model.NewRegister.ImageUser
//     };
//
//     using (var transaction = await _context.Database.BeginTransactionAsync())
//     {
//         try
//         {
//             // Check if this is a new user or update existing one
//             if (Guid.Parse(user.Id) == Guid.Empty)
//             {
//                 // Creating a new user
//                 user.Id = Guid.NewGuid().ToString();
//                 var result = await _userManager.CreateAsync(user, model.NewRegister.Password);
//                 if (true)
//                 {
//                     var roleResult = await _userManager.AddToRoleAsync(user, model.NewRegister.RoleName);
//                     if (true)
//                     {
//                         TempData["msgType"] = "success";
//                         TempData["msg"] = Resource.Resources.lbNotSavedMsgUserRole;
//                         await transaction.CommitAsync();
//                         return RedirectToAction(nameof(Register), "Accounts");
//                     }
//                     else
//                     {
//                         TempData["msgType"] = "error";
//                         TempData["msg"] = Resource.Resources.lbNotSavedMsgUserRole;
//                         await transaction.RollbackAsync();
//                         return View(model);
//                     }
//                 }
//                 else
//                 {
//                     TempData["msgType"] = "error";
//                     TempData["msg"] = Resource.Resources.lbNotSavedMsgUser;
//                     await transaction.RollbackAsync();
//                     return View(model);
//                 }
//             }
//             else
//             {
//                 // Update existing user
//                 var userToUpdate = await _userManager.FindByIdAsync(model.NewRegister.Id.ToString());
//                 if (userToUpdate != null)
//                 {
//                     userToUpdate.Name = model.NewRegister.Name;
//                     userToUpdate.UserName = model.NewRegister.Name;
//                     userToUpdate.Email = model.NewRegister.Email;
//                     userToUpdate.ActiveUser = model.NewRegister.ActiveUser;
//                     userToUpdate.ImageUser = model.NewRegister.ImageUser;
//
//                     var updateResult = await _userManager.UpdateAsync(userToUpdate);
//                     if (updateResult.Succeeded)
//                     {
//                         var currentRoles = await _userManager.GetRolesAsync(userToUpdate);
//                         await _userManager.RemoveFromRolesAsync(userToUpdate, currentRoles);
//
//                         var roleResult = await _userManager.AddToRoleAsync(userToUpdate, model.NewRegister.RoleName);
//                         if (roleResult.Succeeded)
//                         {
//                             TempData["msgType"] = "success";
//                             TempData["msg"] = Resource.Resources.lbNotUpdateMsgUserRole;
//                             await transaction.CommitAsync();
//                         }
//                         else
//                         {
//                             TempData["msgType"] = "error";
//                             TempData["msg"] = Resource.Resources.lbNotUpdateMsgUserRole;
//                             await transaction.RollbackAsync();
//                             return View(model);
//                         }
//                     }
//                     else
//                     {
//                         TempData["msgType"] = "error";
//                         TempData["msg"] = Resource.Resources.lbNotUpdateMsgUser;
//                         await transaction.RollbackAsync();
//                         return View(model);
//                     }
//                 }
//             }
//         }
//         catch (Exception ex)
//         {
//             await transaction.RollbackAsync();
//             TempData["msgType"] = "error";
//             TempData["msg"] = "An unexpected error occurred: " + ex.Message;
//             return View(model);
//         }
//     }
//
//     return RedirectToAction(nameof(Register), "Accounts");
// }


    // [HttpPost]
    // [ValidateAntiForgeryToken]
    // public async Task<IActionResult> Register(RegisterViewModel model)
    // {
    //     // !ModelState.IsValid
    //     if (false)
    //     {
    //         return View(model); // return the same view with validation errors
    //     }
    //
    //     var user = new ApplicationUser
    //     {
    //         Id = model.NewRegister.Id.ToString(),
    //         Name = model.NewRegister.Name,
    //         UserName = model.NewRegister.Email,
    //         Email = model.NewRegister.Email,
    //         ActiveUser = model.NewRegister.ActiveUser,
    //         ImageUser = model.NewRegister.ImageUser
    //     };
    //
    //     // Check if this is a new user or update existing one
    //     if (Guid.Parse(user.Id) == Guid.Empty)
    //     {
    //         // Creating a new user
    //         user.Id = Guid.NewGuid().ToString();
    //         var result = await _userManager.CreateAsync(user, model.NewRegister.Password);
    //         // result.Succeeded
    //         if (true)
    //         {
    //             var roleResult = await _userManager.AddToRoleAsync(user, model.NewRegister.RoleName);
    //             // roleResult.Succeeded
    //             if (true)
    //             {
    //                 TempData["msgType"] = "success";
    //                 TempData["msg"] = Resource.Resources.lbNotSavedMsgUserRole;
    //                 return RedirectToAction(nameof(Register), "Accounts");
    //             }
    //             else
    //             {
    //                 TempData["msgType"] = "error";
    //                 TempData["msg"] = Resource.Resources.lbNotSavedMsgUserRole;
    //             }
    //         }
    //         else
    //         {
    //             TempData["msgType"] = "error";
    //             TempData["msg"] = Resource.Resources.lbNotSavedMsgUser;
    //         }
    //     }
    //     else
    //     {
    //         // Update existing user
    //         var userToUpdate = await _userManager.FindByIdAsync(model.NewRegister.Id.ToString());
    //         if (userToUpdate != null)
    //         {
    //             userToUpdate.Name = model.NewRegister.Name;
    //             userToUpdate.UserName = model.NewRegister.Name;
    //             userToUpdate.Email = model.NewRegister.Email;
    //             userToUpdate.ActiveUser = model.NewRegister.ActiveUser;
    //             userToUpdate.ImageUser = model.NewRegister.ImageUser;
    //
    //             var updateResult = await _userManager.UpdateAsync(userToUpdate);
    //             if (updateResult.Succeeded)
    //             {
    //                 var currentRoles = await _userManager.GetRolesAsync(userToUpdate);
    //                 await _userManager.RemoveFromRolesAsync(userToUpdate, currentRoles);
    //
    //                 var roleResult = await _userManager.AddToRoleAsync(userToUpdate, model.NewRegister.RoleName);
    //                 if (roleResult.Succeeded)
    //                 {
    //                     TempData["msgType"] = "success";
    //                     TempData["msg"] = Resource.Resources.lbNotUpdateMsgUserRole;
    //                 }
    //                 else
    //                 {
    //                     TempData["msgType"] = "error";
    //                     TempData["msg"] = Resource.Resources.lbNotUpdateMsgUserRole;
    //                 }
    //             }
    //             else
    //             {
    //                 TempData["msgType"] = "error";
    //                 TempData["msg"] = Resource.Resources.lbNotUpdateMsgUser;
    //             }
    //         }
    //     }
    //
    //     return RedirectToAction(nameof(Register), "Accounts");
    // }


    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Permissions.Registers.Create)]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        // ModelState.IsValid
        if (true)
        {
            var file = HttpContext.Request.Form.Files;
            if (file.Count() > 0)
            {
                string ImageName = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
                var fileStream = new FileStream(Path.Combine(@"./wwwroot/", Helper.PathSaveImageUser, ImageName),
                    FileMode.Create);
                await file[0].CopyToAsync(fileStream);
                model.NewRegister.ImageUser = ImageName;
            }
            else
            {
                model.NewRegister.ImageUser = model.NewRegister.ImageUser;
            }

            var user = new ApplicationUser
            {
                Id = model.NewRegister.Id.ToString(),
                Name = model.NewRegister.Name,
                UserName = model.NewRegister.Email,
                Email = model.NewRegister.Email,
                ActiveUser = model.NewRegister.ActiveUser,
                ImageUser = model.NewRegister.ImageUser
            };
            if (Guid.Parse(user.Id) == Guid.Empty)
            {
                user.Id = Guid.NewGuid().ToString();
                var result = await _userManager.CreateAsync(user, model.NewRegister.Password);
                if (result.Succeeded)
                {
                    //.GetAwaiter().GetResult()
                    var Role = await _userManager.AddToRoleAsync(user, model.NewRegister.RoleName);
                    if (Role.Succeeded)
                    {
                        HttpContext.Session.SetString("msgType", "success");
                        HttpContext.Session.SetString("title", Resource.Resources.lbSave);
                        HttpContext.Session.SetString("msg", Resource.Resources.lbNotSavedMsgUserRole);
                        return RedirectToAction("Register", "Accounts");
                    }
                    else
                    {
                        HttpContext.Session.SetString("msgType", "error");
                        HttpContext.Session.SetString("title", Resource.Resources.lbNotSaved);
                        HttpContext.Session.SetString("msg", Resource.Resources.lbNotSavedMsgUserRole);
                        return RedirectToAction("Register", "Accounts");
                    }
                }
                else
                {
                    HttpContext.Session.SetString("msgType", "error");
                    HttpContext.Session.SetString("title", Resource.Resources.lbNotSaved);
                    HttpContext.Session.SetString("msg", Resource.Resources.lbNotSavedMsgUser);
                    return RedirectToAction("Register", "Accounts");
                }
            }
            else
            {
                var userupdate = await _userManager.FindByIdAsync(model.NewRegister.Id.ToString());
                userupdate.Id = model.NewRegister.Id.ToString();
                userupdate.Name = model.NewRegister.Name;
                userupdate.UserName = model.NewRegister.Name;
                userupdate.Email = model.NewRegister.Email;
                userupdate.ActiveUser = model.NewRegister.ActiveUser;
                userupdate.ImageUser = model.NewRegister.ImageUser;

                var result = await _userManager.UpdateAsync(userupdate);
                if (result.Succeeded)
                {
                    var oldRoles = await _userManager.GetRolesAsync(userupdate);
                    await _userManager.RemoveFromRolesAsync(userupdate, oldRoles);
                    var addroles = await _userManager.AddToRoleAsync(userupdate, model.NewRegister.RoleName);

                    if (addroles.Succeeded)
                    {
                        HttpContext.Session.SetString("msgType", "success");
                        HttpContext.Session.SetString("title", Resource.Resources.lbUpdate);
                        HttpContext.Session.SetString("msg", Resource.Resources.lbNotUpdateMsgUserRole);
                        return RedirectToAction("Register", "Accounts");
                    }
                    else
                    {
                        HttpContext.Session.SetString("msgType", "error");
                        HttpContext.Session.SetString("title", Resource.Resources.lbNotUpdate);
                        HttpContext.Session.SetString("msg", Resource.Resources.lbNotUpdateMsgUserRole);
                        return RedirectToAction("Register", "Accounts");
                    }
                }
                else
                {
                    HttpContext.Session.SetString("msgType", "error");
                    HttpContext.Session.SetString("title", Resource.Resources.lbNotUpdate);
                    HttpContext.Session.SetString("msg", Resource.Resources.lbNotUpdateMsgUser);
                    return RedirectToAction("Register", "Accounts");
                }
            }

            return RedirectToAction("Register", "Accounts");
        }

        return RedirectToAction("Register", "Accounts");
    }


    // public async Task<IActionResult> DeleteUser(string Id)
    // {
    //     // Find the user by Id
    //     var user = await _userManager.FindByIdAsync(Id);
    //
    //     // Check if the user exists
    //     if (user == null)
    //     {
    //         return RedirectToAction("Register", "Accounts");
    //     }
    //
    //     // Check if the user has an image and if it's a valid GUID
    //     if (!string.IsNullOrEmpty(user.ImageUser))
    //     {
    //         string imageGuid = Path.GetFileNameWithoutExtension(user.ImageUser);
    //
    //         if (Guid.TryParse(imageGuid, out Guid parsedGuid) && parsedGuid != Guid.Empty)
    //         {
    //             var imagePath = Path.Combine("./wwwroot/", Helper.PathImageUser, user.ImageUser);
    //
    //             // Check if the file exists
    //             if (System.IO.File.Exists(imagePath))
    //             {
    //                 // Delete the user
    //                 var deleteResult1 = await _userManager.DeleteAsync(user);
    //
    //                 if (deleteResult1.Succeeded)
    //                 {
    //                     // Delete the image file
    //                     System.IO.File.Delete(imagePath);
    //
    //                     return RedirectToAction("Register", "Accounts");
    //                 }
    //                 else
    //                 {
    //                     return RedirectToAction("Register", "Accounts");
    //                 }
    //
    //                 return RedirectToAction("Register", "Accounts");
    //             }
    //         }
    //     }
    //
    //     // If no image or invalid GUID, just delete the user
    //     var deleteResult = await _userManager.DeleteAsync(user);
    //     if (deleteResult.Succeeded)
    //     {
    //         return RedirectToAction("Register", "Accounts");
    //     }
    //     else
    //     {
    //         return RedirectToAction("Register", "Accounts");
    //     }
    //
    //     return RedirectToAction("Register", "Accounts");
    // }


    // public async Task<IActionResult> DeleteUser(string Id)
    // {
    //     var userid = await _userManager.FindByIdAsync(Id);
    //     if (userid != null)
    //     {
    //         var user = _userManager.Users.FirstOrDefault(x => x.Id == Id);
    //         string imageGuid = Path.GetFileNameWithoutExtension(user.ImageUser).ToString();
    //         if (user.ImageUser != null && Guid.Parse(imageGuid) != Guid.Empty)
    //         {
    //             var imagePath = Path.Combine("./wwwroot/", imageGuid, user.ImageUser);
    //             if (System.IO.File.Exists(imagePath))
    //             {
    //                 if ((await _userManager.DeleteAsync(user)).Succeeded)
    //                 {
    //                     System.IO.File.Delete(imagePath);
    //                     return RedirectToAction("Register", "Accounts");
    //                 }
    //                 else
    //                 {
    //                     return RedirectToAction("Register", "Accounts");
    //                 }
    //             }
    //             else
    //             {
    //                 return RedirectToAction("Register", "Accounts");
    //             }
    //         }
    //         else
    //         {
    //             return RedirectToAction("Register", "Accounts");
    //         }
    //     }
    //     
    //     return RedirectToAction("Register", "Accounts");
    // }


    // public async Task<IActionResult> DeleteUser(string userId)
    // {
    //     var user = _userManager.Users.FirstOrDefault(x => x.Id == userId);
    //
    //     if (user.ImageUser != null && Guid.Parse(Path.GetFileNameWithoutExtension(user.ImageUser)) != Guid.Empty)
    //     {
    //         var PathImage = Path.Combine(@"wwwroot/", Helper.PathImageUser, user.ImageUser);
    //         if (System.IO.File.Exists(PathImage))
    //             System.IO.File.Delete(PathImage);
    //         return RedirectToAction("Register", "Accounts");
    //     }
    //     if ((await _userManager.DeleteAsync(user)).Succeeded)
    //         return RedirectToAction("Register", "Accounts");
    //
    //     return RedirectToAction("Register", "Accounts");
    // }

    
    [Authorize(Permissions.Accounts.Delete)]
    public async Task<IActionResult> DeleteUser(string Id)
    {
        var user = _userManager.Users.FirstOrDefault(x => x.Id == Id);
        // var result = await _userManager.DeleteAsync(user);
        // var imageGuid = Path.GetFileNameWithoutExtension(user.ImageUser);
        if (user.ImageUser != null && Guid.Parse((user.ImageUser.ToString().Substring(0, user.ImageUser.Length - 4))) !=
            Guid.Empty)
        {
            var PathImage = Path.Combine(@"./wwwroot/", Helper.PathImageUser, user.ImageUser);
            if (System.IO.File.Exists(PathImage))
            {
                System.IO.File.Delete(PathImage);
            }
        }

        if ((await _userManager.DeleteAsync(user)).Succeeded)
        {
            return RedirectToAction("Register", "Accounts");
        }

        return RedirectToAction("Register", "Accounts");
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Permissions.Accounts.Create)]
    public async Task<IActionResult> ChangePassword(RegisterViewModel model)
    {
        var user = _userManager.FindByIdAsync(model.ChangePassword.Id.ToString());
        if (user != null)
        {
            await _userManager.RemovePasswordAsync(await user);
            var AddNewPassword = await _userManager.AddPasswordAsync(await user, model.ChangePassword.NewPassword);
            if (AddNewPassword.Succeeded)
            {
                HttpContext.Session.SetString("msgType", "success");
                HttpContext.Session.SetString("title", Resource.Resources.lbSave);
                HttpContext.Session.SetString("msg", Resource.Resources.lbMsgSavedChangePassword);
            }
            else
            {
                HttpContext.Session.SetString("msgType", "error");
                HttpContext.Session.SetString("title", Resource.Resources.lbNotSaved);
                HttpContext.Session.SetString("msg", Resource.Resources.lbMsgNotSavedChangePassword);
            }

            return RedirectToAction(nameof(Register));
        }

        return RedirectToAction(nameof(Register));
    }


    [AllowAnonymous]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        // ModelState.IsValid
        if (true)
        {
            var Resualt =
                await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            if (Resualt.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ErrorLogin = false;
            }

            return View(model);
        }
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    [AllowAnonymous]
    public async Task<IActionResult> Logout(LoginViewModel model)
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction(nameof(Login));
    }

    #endregion
}