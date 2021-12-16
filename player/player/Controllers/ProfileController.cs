using Microsoft.AspNetCore.Mvc;
using player.Attributes;
using player.DB;

namespace player.Controllers;

public class ProfileController : Controller
{
    [Authorize, HttpPost]
    public IActionResult Edit(User user)
    {
        var currentUser = (User)HttpContext.Items["User"]!;
        if (user.Login != string.Empty)
            currentUser.Login = user.Login;
        if (user.Name != string.Empty)
            currentUser.Name = user.Name;
        if (user.Surname != string.Empty)
            currentUser.Surname = user.Surname;
        if (user.PhoneNumber != default)
            currentUser.PhoneNumber = user.PhoneNumber;
        return Ok();
    }
}
