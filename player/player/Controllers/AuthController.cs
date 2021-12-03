using Microsoft.AspNetCore.Mvc;
using player.Backend;
using player.Backend.Models;

namespace player.Controllers
{
    public class AuthController : Controller
    {
        [Route("[controller]")]
        public class EnteringController : Controller
        {
            private readonly JwtAuthManager _jwtAuthManager;

            public EnteringController(JwtAuthManager jwtAuthManager) =>
                _jwtAuthManager = jwtAuthManager;

            [HttpPost, Route(nameof(Login))]
            public ActionResult Login([FromBody] AuthData user) =>
                Connector.Authenticate(user.Login, user.Password)
                    ? Ok($"Welcome. Your token: {_jwtAuthManager.GetToken(user.Login)}")
                        : Ok("Wrong Login or Password");

            [HttpPost, Route(nameof(Register))]
            public ActionResult Register([FromBody] AuthData user) =>
                 Connector.Register(user.Login, user.Password)
                    ? Ok($"Welcome. Your token: {_jwtAuthManager.GetToken(user.Login)}")
                        : Ok("You already regiser.");
        }
    }

}
}