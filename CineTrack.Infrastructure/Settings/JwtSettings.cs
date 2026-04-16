using System.ComponentModel.DataAnnotations;

namespace CineTrack.Infrastructure.Settings;

public class JwtSettings
{
    [Required, Url]
    public string Issuer { get; init; } = null!;
    
    [Required, Url]
    public string Audience { get; init; } = null!;
    
    [Required, MinLength(32, ErrorMessage = "Secret Key must be at least 32 characters long")]
    public string SecretKey { get; init; } = null!;
}