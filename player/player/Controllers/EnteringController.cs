using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using player.DB;
using player.Services;

namespace player.Controllers;

public class EnteringController : ControllerBase
{
    private readonly PlayerContext _dataContext;

    public EnteringController(PlayerContext dataContext) =>
        _dataContext = dataContext;

    [HttpPost]
    public string Login([FromForm] UserLoginPassword logPass)
    {
        var user = DbHelper.GetUserWithId(logPass.Login, logPass.Password, _dataContext);
        return user is null
            ? string.Empty
            : JwtGenerator.GenerateJwtToken(user.Id);
    }

    [HttpPost]
    public IActionResult Register([FromForm] UserLoginPassword logPass)
    {
        var user = new User
        {
            Login = logPass.Login,
            Password = logPass.Password
        };
        Expression<Func<User, bool>> searchingExpression = u => u.Login == user.Login;
        if (_dataContext.Users.Any(searchingExpression))
            throw new Exception("already registered");
        _dataContext.Users.Add(user);
        _dataContext.SaveChanges();
        user = _dataContext.Users.FirstOrDefault(searchingExpression);
        var token = JwtGenerator.GenerateJwtToken(user!.Id);
        Console.WriteLine(token);
        return new JsonResult(token);
    }

    public class UserLoginPassword
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
