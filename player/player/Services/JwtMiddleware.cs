using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using player.DB;

namespace player.Services;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;

    private const string Secret = "123123123123123123123123123123";
    private static readonly byte[] key = Encoding.ASCII.GetBytes(Secret);

    public JwtMiddleware(RequestDelegate next) =>
        _next = next;

    public async Task Invoke(HttpContext context, PlayerContext dataContext)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        if (token is not null)
            await AttachAccountToContext(context, dataContext, token);
        await _next(context);
    }

    private async Task AttachAccountToContext(HttpContext context, PlayerContext dataContext, string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        tokenHandler.ValidateToken(token, new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        }, out var validatedToken);

        var jwtToken = validatedToken as JwtSecurityToken;
        var accountId = int.Parse(jwtToken!.Claims.First(x => x.Type is "id").Value);

        context.Items["User"] = await dataContext.Users.FindAsync(accountId);
    }
}
