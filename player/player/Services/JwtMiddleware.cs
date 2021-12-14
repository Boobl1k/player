using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using player.DB;

namespace player.Services;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;

    private const string Secret = "123123123123123";
    private static readonly byte[] key = Encoding.ASCII.GetBytes(Secret);

    public JwtMiddleware(RequestDelegate next) =>
        _next = next;

    public async Task Invoke(HttpContext context, IDbContext<User> dataContext)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        if (token is not null)
            await AttachAccountToContext(context, dataContext, token);
        await _next(context);
    }

    public string GenerateJwtToken(int accountId)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] {new Claim("id", accountId.ToString())}),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private async Task AttachAccountToContext(HttpContext context, IDbContext<User> dataContext, string token)
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

        var jwtToken = (JwtSecurityToken) validatedToken;
        var accountId = int.Parse(jwtToken.Claims.First(x => x.Type is "id").Value);

        context.Items["User"] = await dataContext.Items.FindAsync(accountId);
    }
}
