using System.Linq;
using Microsoft.AspNetCore.Mvc;
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
}
