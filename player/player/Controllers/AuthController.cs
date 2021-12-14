using Microsoft.AspNetCore.Mvc;

namespace player.Controllers;

public class AuthController : Controller
{
    // GET
    public IActionResult Login() => 
        View();

    public IActionResult Register() => 
        View();
}
