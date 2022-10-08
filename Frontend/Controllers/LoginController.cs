using Microsoft.AspNetCore.Mvc;

namespace Frontend.Controllers;

public class LoginController : Controller
{
    // GET
    public IActionResult Login()
    {
        return View();
    }
}