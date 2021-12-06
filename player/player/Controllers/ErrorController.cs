using Microsoft.AspNetCore.Mvc;

namespace player.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
        [Route("404")]
        public IActionResult PageNotFound()
        {
            return View();
        }
    }
}