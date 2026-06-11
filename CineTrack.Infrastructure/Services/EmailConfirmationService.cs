using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using CineTrack.App.Common.Settings;
using CineTrack.App.Interfaces;
using CineTrack.Domain.Entities;
using CineTrack.Infrastructure.Settings;

namespace CineTrack.Infrastructure.Services;

public class EmailConfirmationService(
    IMailSender mailSender, 
    IOptions<WebSiteSettings> webSiteOptions,
    IOptions<AuthSettings> authOptions,
    ILogger<EmailConfirmationService> logger) : IEmailConfirmationService
{
    private const string EmailSubject = "CineTrack: Confirm registration";
    private const string EmailBodyTemplate = """
                         Thank you for your registration to the CineTrack website.
                         To finish the registration, please click on the link below:
                         {0}
                         The link will expire in {1} minute(s).

                         Best regards,
                         CineTrack Team
                         """;
    
    public void GenerateConfirmationToken(User user)
    {
        var authSettings = authOptions.Value;
        var token = Guid.NewGuid();
        user.EmailConfirmationToken = token;
        user.EmailConfirmationTokenExpiresAt = DateTime.UtcNow.AddMinutes(authSettings.EmailConfirmationTokenExpirationMinutes);
        user.IsEmailConfirmed = false;
    }

    public async Task SendConfirmationEmailAsync(User user)
    {
        var webSiteSettings = webSiteOptions.Value;
        var authSettings = authOptions.Value;
        var webSiteUrl = webSiteSettings.BaseUrl;
        var emailConfirmationPath = webSiteSettings.EmailConfirmationPath;
        
        var confirmationLink = $"{webSiteUrl}{emailConfirmationPath}?token={user.EmailConfirmationToken}";
        var emailBody = string.Format(EmailBodyTemplate, confirmationLink, authSettings.EmailConfirmationTokenExpirationMinutes);
        
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