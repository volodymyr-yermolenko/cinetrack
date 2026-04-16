using System.ComponentModel.DataAnnotations;

namespace CineTrack.Infrastructure.Settings;

public class EmailSettings
{
    [Required] 
    public string SmtpServer { get; init; } = null!;
    
    [Range(1, 65535)]
    public int SmtpPort { get; init; }

    [Required, EmailAddress] 
    public string SenderEmail { get; init; } = null!;

    [Required, StringLength(16, MinimumLength = 16, ErrorMessage = "Application password must be 16 characters long.")]
    public string AppPassword { get; init; } = null!;
}