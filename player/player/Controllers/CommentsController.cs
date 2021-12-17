using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using player.Attributes;
using player.DB;

namespace player.Controllers;

public class CommentsController : Controller
{
    private readonly PlayerContext _dataContext;

    public CommentsController(PlayerContext dataContext) =>
        _dataContext = dataContext;

    [Authorize, HttpPost]
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

    [Authorize, HttpPost]
    public IActionResult Remove(int commentId)
    {
        Console.WriteLine($"removing comment {commentId}");
        var currentUser = HttpContext.Items["User"] as User;
        var comment = _dataContext.Comments.FirstOrDefault(c => c.Id == commentId);
        if (comment is null || (comment.WriterId != currentUser!.Id && comment.UserId != currentUser.Id))
            return BadRequest();
        _dataContext.Comments.Remove(comment);
        _dataContext.SaveChanges();
        return Ok();
    }
}
