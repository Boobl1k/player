using Microsoft.AspNetCore.Mvc;
using player.Attributes;
using player.DB;
using player.Services;

namespace player.Controllers;

[ApiController]
[Route("[controller]")]
public class QweController : ControllerBase
{
    private PlayerContext _dataContext;

    public QweController(PlayerContext dataContext) =>
        _dataContext = dataContext;

    [HttpPost]
    [Route("qwe")]
    public string Login(
        [FromForm] AuthController.UserLoginPassword logPass)
    {
        var user = DbHelper.GetUserWithId(logPass.Login, logPass.Password, _dataContext);
        return JwtGenerator.GenerateJwtToken(123);
    }

    [HttpPost("Register")]
    [Authorize]
    public string Register(
        [FromForm]AuthController.UserLoginPassword logPass)
    {
        var user = new User
        {
            Login = logPass.Login,
            Password = logPass.Password
        };
        _dataContext.Users.Add(user);
        user = _dataContext.Update(user).Entity;
        _dataContext.SaveChanges();
        return JwtGenerator.GenerateJwtToken(user.Id);
    }
}
