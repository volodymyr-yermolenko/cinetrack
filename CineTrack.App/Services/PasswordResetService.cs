using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using CineTrack.App.Common.Settings;
using CineTrack.App.Interfaces;
using CineTrack.Domain.Entities;

namespace CineTrack.App.Services;

public class PasswordResetService(
    IMailSender mailSender, 
    IOptions<WebSiteSettings> options, 
    ILogger<PasswordResetService> logger)
{
    private const int TokenExpirationMinutes = 15;
    private const string EmailSubject = "CineTrack: Reset password";
    private const string EmailBodyTemplate = """
                                             You have requested to reset your password for the CineTrack website.
                                             Please click on the link below to reset your password:
                                             {0}
                                             The link will expire in {1} minute(s).

                                             Best regards,
                                             CineTrack Team
                                             """;
    
    public void GenerateResetToken(User user)
    {
        var token = Guid.NewGuid();
        user.PasswordResetToken = token;
        user.PasswordResetTokenExpiresAt = DateTime.UtcNow.AddMinutes(TokenExpirationMinutes);
    }

    public async Task SendResetEmailAsync(User user)
    {
        var webSiteSettings = options.Value;
        var webSiteUrl = webSiteSettings.BaseUrl;
        var passwordResetPath = webSiteSettings.PasswordResetPath;
        
        var resetLink = $"{webSiteUrl}{passwordResetPath}?token={user.PasswordResetToken}";
        var emailBody = string.Format(EmailBodyTemplate, resetLink, TokenExpirationMinutes);
        
        try
        {
            await mailSender.SendEmailAsync(user.Email, EmailSubject, emailBody);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error sending email");
        }
    }
}