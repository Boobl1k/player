using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using player.Attributes;
using player.DB;
using player.Models;

namespace player.Controllers;

public class HomeController : Controller
{
    private readonly PlayerContext _dataContext;

    public HomeController(PlayerContext dataContext) =>
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
        var currentUser = HttpContext.Items["User"] as User;
        var user = userId is default(int)
            ? currentUser
            : _dataContext.Users.FirstOrDefault(u => u.Id == userId);
        if (user is null) return BadRequest();
        var comments = _dataContext.Comments
            .Where(c => c.UserId == user.Id)
            .Select(c => new BetterComment
            {
                Text = c.Text,
                Writer = _dataContext.Users.FirstOrDefault(u => u.Id == c.WriterId)!
            }).ToList();

        return View(new ProfileModel
        {
            IsCurrentUser = currentUser!.Id == user.Id,
            User = user,
            Comments = comments
        });
    }

    public readonly struct BetterComment
    {
        public string Text { get; init; }
        public User Writer { get; init; }
    }

    public readonly struct ProfileModel
    {
        public bool IsCurrentUser { get; init; }
        public User User { get; init; } = null!;
        public List<BetterComment> Comments { get; init; } = null!;
    }

    [Authorize, HttpGet]
    public IActionResult EditProfile() =>
        View(HttpContext.Items["User"]);

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

    [HttpGet]
    public IActionResult Users(int page)
    {
        ViewBag.Page = page;
        return View(_dataContext.Users.Skip(7 * page).Take(7));
    }
}
