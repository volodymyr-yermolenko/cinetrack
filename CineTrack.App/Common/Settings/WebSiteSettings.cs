using System.ComponentModel.DataAnnotations;

namespace CineTrack.App.Common.Settings;

public class WebSiteSettings
{
    [Required, Url] 
    public string BaseUrl { get; init; } = null!;

    [Required, MinLength(2)] 
    public string EmailConfirmationPath { get; init; } = null!;

    [Required, MinLength(2)] 
    public string PasswordResetPath { get; init; } = null!;
}