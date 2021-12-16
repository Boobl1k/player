using System;
using System.Collections.Generic;
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
    public IActionResult Login([FromForm] UserLoginPassword logPass)
    {
        var user = DbHelper.GetUserWithId(logPass.Login, logPass.Password, _dataContext);
        if (user is not null)
        {
            Console.WriteLine("writing cookies");
            HttpContext.Response.Cookies.Append("Authorization", JwtGenerator.GenerateJwtToken(user.Id));
        }
        return user is null
            ? new JsonResult(string.Empty)
            : new JsonResult(JwtGenerator.GenerateJwtToken(user.Id));
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
