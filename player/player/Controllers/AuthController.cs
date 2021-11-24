using Microsoft.AspNetCore.Mvc;

namespace player.Controllers
{
    public class AuthController : Controller
    {
        // GET
        public IActionResult Login()
        {
            return View();
        }
        
        public IActionResult Register()
        {
            return View();
        }
    }
}