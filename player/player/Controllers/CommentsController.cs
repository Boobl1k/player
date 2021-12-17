using Microsoft.AspNetCore.Mvc;
using player.Attributes;
using player.DB;

namespace player.Controllers;

public class CommentsController : Controller
{
    private readonly PlayerContext _dataContext;

    public CommentsController(PlayerContext dataContext) => 
        _dataContext = dataContext;

    [Authorize,HttpPost]
    public IActionResult Comment(int userId, string text)
    {
        _dataContext.Comments.Add(new Comment
        {
            UserId = userId,
            WriterId = (HttpContext.Items["User"] as User)!.Id,
            Text = text
        });
        _dataContext.SaveChanges();
        return Ok();
    }
}
