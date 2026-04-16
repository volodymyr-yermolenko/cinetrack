using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using CineTrack.Infrastructure.Common.Helpers;
using CineTrack.Infrastructure.Settings;

namespace CineTrack.Infrastructure.Services;

public class JwtTokenService(IOptions<JwtSettings> options)
{
    public string? GenerateAccessToken(string userName)
    {
        var settings = options.Value;
        var claims = new List<Claim>
        {
            new (ClaimTypes.Name, userName)
        };

        var securityKey = SecurityKeyHelper.GetSymmetricSecurityKey(settings.SecretKey);
        var token = new JwtSecurityToken(
            issuer: settings.Issuer,
            audience: settings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(30),
            signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
        );
        
        var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
        return accessToken;
    }
}