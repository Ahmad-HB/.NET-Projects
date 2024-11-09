using Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreeBooks.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Permissions.Home.View)]
public class HomeController : Controller
{
    // GET
    [Authorize(Permissions.Home.View)]
    public IActionResult Index()
    {
        return View();
    }
    [AllowAnonymous]
    public IActionResult Denied()
    {
        return View();
    }
}