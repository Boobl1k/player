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

    [HttpGet]
    public IActionResult Search(string searchingRequest, int page)
    {
        ViewBag.SearchingRequest = searchingRequest;
        ViewBag.Page = page;
        var tracks = _dataContext.Tracks
            .Where(t => t.Author.Contains(searchingRequest) || t.Name.Contains(searchingRequest))
            .Skip(page * 7)
            .Take(7);
        return View(tracks);
    }

    [Authorize, HttpGet]
    public IActionResult Liked()
    {
        var userId = (HttpContext.Items["User"] as User)!.Id;
        var tracks = _dataContext.Tracks
            .Where(t =>
            _dataContext.LikedTracks
                .Where(lt => lt.UserId == userId)
                .Select(lt => lt.TrackId)
                .Contains(t.Id));
        return View(tracks);
    }
}
