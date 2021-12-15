using System.Linq;
using Microsoft.AspNetCore.Mvc;
using player.DB;
using player.Services;

namespace player.Controllers;

[ApiController]
[Route("[controller]")]
public class EnteringController : ControllerBase
{
    private readonly PlayerContext _dataContext;

    public EnteringController(PlayerContext dataContext) =>
        _dataContext = dataContext;

    [HttpPost("Login")]
    public string Login([FromForm] UserLoginPassword logPass)
    {
        var user = DbHelper.GetUserWithId(logPass.Login, logPass.Password, _dataContext);
        return user is null
            ? string.Empty
            : JwtGenerator.GenerateJwtToken(user.Id);
    }

    [HttpPost("Register")]
    public string Register([FromForm] UserLoginPassword logPass)
    {
        var user = new User
        {
            Login = logPass.Login,
            Password = logPass.Password
        };
        _dataContext.Users.Add(user);
        _dataContext.SaveChanges();
        user = _dataContext.Users.FirstOrDefault(u => u.Login == user.Login && u.Password == user.Password);
        return JwtGenerator.GenerateJwtToken(user!.Id);
    }

    public class UserLoginPassword
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
