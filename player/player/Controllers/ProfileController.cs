using System.Linq;
using Microsoft.AspNetCore.Mvc;
using player.Attributes;
using player.DB;

namespace player.Controllers;

public class ProfileController : Controller
{
    private readonly PlayerContext _dataContext;

    public ProfileController(PlayerContext dataContext) =>
        _dataContext = dataContext;

    [Authorize, HttpPost]
    public IActionResult Edit([FromForm] User user)
    {
        var currentUser = (User) HttpContext.Items["User"]!;
        if (user.Login != string.Empty && user.Login is not null && _dataContext.Users.All(u => u.Login != user.Login))
            currentUser.Login = user.Login;
        if (user.Name != string.Empty && user.Name is not null)
            currentUser.Name = user.Name;
        if (user.Surname != string.Empty && user.Surname is not null)
            currentUser.Surname = user.Surname;
        if (user.PhoneNumber != default && _dataContext.Users.All(u => u.PhoneNumber != user.PhoneNumber))
            currentUser.PhoneNumber = user.PhoneNumber;
        _dataContext.Update(currentUser);
        _dataContext.SaveChanges();
        return Ok();
    }
}
