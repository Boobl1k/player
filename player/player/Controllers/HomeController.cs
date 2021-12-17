﻿using System.Collections.Generic;
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
        var user = userId is default(int)
            ? HttpContext.Items["User"] as User
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
        public User User { get; init; } = null!;
        public List<BetterComment> Comments { get; init; } = null!;
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
