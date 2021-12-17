using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using player.Attributes;
using player.DB;
using player.Models;

namespace player.Controllers;

public class HomeController : Controller
{
    private readonly PlayerContext _dataContext;

    public HomeController(ILogger<HomeController> logger, PlayerContext dataContext) =>
        _dataContext = dataContext;

    [HttpGet]
    public IActionResult Index(int page)
    {
        ViewBag.Page = page;
        return View(_dataContext.Tracks.Skip(7 * page).Take(7));
    }

    [HttpGet]
    public IActionResult Privacy() =>
        View();

    [HttpGet]
    public IActionResult Profile(int userId)
    {
        //var comments = _dataContext.Comments.Where(c => c.UserId)
        return View(userId is default(int)
            ? HttpContext.Items["User"]
            : _dataContext.Users.FirstOrDefault(u => u.Id == userId));
    }
    
    public class ProfileModel
    {
        public User? User { get; set; }
        public IQueryable<Comment>? Comments { get; set; }
    }

    [Authorize, HttpGet]
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
