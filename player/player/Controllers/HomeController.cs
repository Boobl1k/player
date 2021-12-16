using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using player.Attributes;
using player.Models;

namespace player.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger) =>
        _logger = logger;


    [HttpGet]
    public IActionResult Index() =>
        View();

    [HttpGet]
    [Authorize]
    public IActionResult Privacy()
    {
        return View();
    }


    [HttpGet]
    public IActionResult Profile() =>
        View();


    [HttpGet]
    public IActionResult EditProfile() =>
        View();


    [HttpGet]
    public IActionResult Search() =>
        View();


    [HttpGet]
    public IActionResult Contact() =>
        View();


    [HttpGet]
    public IActionResult About() =>
        View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() =>
        View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
}
