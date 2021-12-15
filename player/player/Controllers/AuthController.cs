using Microsoft.AspNetCore.Mvc;

namespace player.Controllers;

public class AuthController : Controller
{
    [HttpGet]
    public IActionResult LoginPage() =>
        View();

    [HttpGet]
    public IActionResult RegisterPage() =>
        View();
}
