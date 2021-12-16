using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using player.Attributes;
using player.DB;
using player.Models;

namespace player.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly PlayerContext _dataContext;

    public HomeController(ILogger<HomeController> logger, PlayerContext dataContext)
    {
        _logger = logger;
        _dataContext = dataContext;
    }

    [HttpGet]
    public IActionResult Index(int page)
    {
        ViewBag.Page = page;
        ViewBag.Tracks = _dataContext.Tracks;
        return View();
    }

    [HttpGet]
    public IActionResult Privacy() =>
        View();

    [Authorize]
    [HttpGet]
    public IActionResult Profile()
    {
        return View(HttpContext.Items["User"]);
    }

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

    [HttpGet]
    public IActionResult AddMusicPage() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() =>
        View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
}
