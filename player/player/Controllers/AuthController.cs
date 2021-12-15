using Microsoft.AspNetCore.Mvc;
using player.Attributes;
using player.DB;
using player.Services;

namespace player.Controllers;

public class AuthController : Controller
{


    [HttpGet]
    public IActionResult LoginPage() =>
        View();

    [HttpGet]
    public IActionResult RegisterPage() =>
        View();

    public class UserLoginPassword
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
    
    
}
