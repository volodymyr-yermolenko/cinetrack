using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using CineTrack.App.Common.Settings;
using CineTrack.App.Interfaces;
using CineTrack.Domain.Entities;
using CineTrack.Infrastructure.Settings;

namespace CineTrack.Infrastructure.Services;

public class PasswordResetService(
    IMailSender mailSender, 
    IOptions<WebSiteSettings> webSiteOptions, 
    IOptions<AuthSettings> authOptions,
    ILogger<PasswordResetService> logger) : IPasswordResetService
{
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
        var authSettings = authOptions.Value;
        var token = Guid.NewGuid();
        user.PasswordResetToken = token;
        user.PasswordResetTokenExpiresAt = DateTime.UtcNow.AddMinutes(authSettings.ResetPasswordTokenExpirationMinutes);
    }

    public async Task SendResetEmailAsync(User user)
    {
        var webSiteSettings = webSiteOptions.Value;
        var authSettings = authOptions.Value;
        var webSiteUrl = webSiteSettings.BaseUrl;
        var passwordResetPath = webSiteSettings.PasswordResetPath;
        
        var resetLink = $"{webSiteUrl}{passwordResetPath}?token={user.PasswordResetToken}";
        var emailBody = string.Format(EmailBodyTemplate, resetLink, authSettings.ResetPasswordTokenExpirationMinutes);
        
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