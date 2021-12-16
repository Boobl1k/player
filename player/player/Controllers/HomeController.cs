using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        //HttpContext.Request.Cookies.
        Console.WriteLine($"User: {HttpContext.Items["User"]}");
        ViewBag.User = HttpContext.Items["User"]!;
        ViewBag.Page = page;
        ViewBag.Tracks = _dataContext.Tracks;
        return View();
    }

    [HttpGet]
    public IActionResult Privacy() =>
        View();

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

    [HttpGet]
    public IActionResult AddMusicPage() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() =>
        View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
}
