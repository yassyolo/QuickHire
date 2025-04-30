using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using QuickHire.Infrastructure.Options;
using QuickHire.Infrastructure.Persistence.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace QuickHire.Infrastructure.Authentication.Processors;

internal class AuthTokenProcessor : IAuthTokenProcessor
{ 
    private readonly JwtOptions _jwtOptions;
    private readonly IHttpContextAccessor _contextAccessor;

    public AuthTokenProcessor(IOptions<JwtOptions> jwtOptions, IHttpContextAccessor httpContext)
    {
        _jwtOptions = jwtOptions.Value;
        _contextAccessor = httpContext;
    }

    public (string jwtToken, DateTime expirationTime) GenerateToken(ApplicationUser user, IList<string> roles)
    {
        var symmetricKey = new SymmetricSecurityKey
                           (Encoding.UTF8.GetBytes(_jwtOptions.Secret));

        var signingCredentials = new SigningCredentials
                                 (symmetricKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
            new Claim(ClaimTypes.NameIdentifier, user.UserName),
        };

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var expiresAt = DateTime.Now.AddMinutes(double.Parse(_jwtOptions.ExpirationTimeInMinutes));

        var token = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: claims,
            signingCredentials: signingCredentials,
            expires: expiresAt
        );

        var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

        return (jwtToken, expiresAt);
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public void WriteTokeToCookie(string cookieName, string token, DateTime expiresAt)
    {
        _contextAccessor.HttpContext?.Response.Cookies.Append(cookieName, token,
             new CookieOptions
             {
                 HttpOnly = true,
                 Expires = expiresAt,
                 Secure = true,
                 IsEssential = true,
                 SameSite = SameSiteMode.Strict
             });
    }
}
