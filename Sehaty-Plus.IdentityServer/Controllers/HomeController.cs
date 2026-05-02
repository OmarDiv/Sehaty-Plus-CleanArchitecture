using Microsoft.AspNetCore.Mvc;

namespace Sehaty_Plus.IdentityServer.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return RedirectToAction("Login", "Account");
    }
}
