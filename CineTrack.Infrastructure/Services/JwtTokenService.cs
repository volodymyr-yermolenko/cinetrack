using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using CineTrack.App.Interfaces;
using CineTrack.Domain.Entities;
using CineTrack.Infrastructure.Common.Helpers;
using CineTrack.Infrastructure.Settings;

namespace CineTrack.Infrastructure.Services;

public class JwtTokenService(IOptions<JwtSettings> options) : ITokenService
{
    private static readonly JwtSecurityTokenHandler Handler = new();
    
    public string? GenerateAccessToken(User user)
    {
        var settings = options.Value;
        var claims = new List<Claim>
        {
            new (ClaimTypes.Name, user.Name),
            new (ClaimTypes.Email, user.Email),
            new (ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        var securityKey = SecurityKeyHelper.GetSymmetricSecurityKey(settings.SecretKey);
        var token = new JwtSecurityToken(
            issuer: settings.Issuer,
            audience: settings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(30),
            signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
        );
        
        var accessToken = Handler.WriteToken(token);
        return accessToken;
    }
}