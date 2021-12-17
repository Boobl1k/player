using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using player.Attributes;
using player.DB;

namespace player.Controllers;

public class TracksController : Controller
{
    private readonly PlayerContext _dataContext;

    public TracksController(PlayerContext dataContext) =>
        _dataContext = dataContext;

    [HttpPost]
    public IActionResult Add(Track track)
    {
        _dataContext.Tracks.Add(track);
        _dataContext.SaveChanges();
        return Ok();
    }
    
    [HttpGet]
    public IActionResult Search(string searchingRequest, int page, string onlyAuthor)
    {
        Console.WriteLine(onlyAuthor);
        ViewBag.SearchingRequest = searchingRequest;
        ViewBag.Page = page;
        var tracks = onlyAuthor == "on"
            ? _dataContext.Tracks
                .Where(t => t.Author.Contains(searchingRequest))
                .Skip(page * 7)
                .Take(7)
            : _dataContext.Tracks
                .Where(t => t.Author.Contains(searchingRequest) || t.Name.Contains(searchingRequest))
                .Skip(page * 7)
                .Take(7);
        return View(tracks);
    }

    [Authorize, HttpGet]
    public IActionResult Liked(int page)
    {
        ViewBag.Page = page;
        var userId = (HttpContext.Items["User"] as User)!.Id;
        var tracks = _dataContext.Tracks
            .Where(t =>
                _dataContext.LikedTracks
                    .Where(lt => lt.UserId == userId)
                    .Select(lt => lt.TrackId)
                    .Contains(t.Id))
            .Skip(7 * page)
            .Take(7);
        return View(tracks);
    }

    [Authorize, HttpPost]
    public IActionResult Like(int trackId)
    {
        var userId = (HttpContext.Items["User"] as User)!.Id;
        _dataContext.LikedTracks.Add(new LikedTrack
        {
            UserId = userId,
            TrackId = trackId
        });
        _dataContext.SaveChanges();
        return Ok();
    }

    [Authorize, HttpPost]
    public IActionResult Unlike(int trackId)
    {
        var userId = (HttpContext.Items["User"] as User)!.Id;
        var liked = _dataContext.LikedTracks.FirstOrDefault(lt => lt.UserId == userId && lt.TrackId == trackId);
        if (liked is null) return BadRequest();
        _dataContext.LikedTracks.Remove(liked);
        _dataContext.SaveChanges();
        return Ok();
    }
}
