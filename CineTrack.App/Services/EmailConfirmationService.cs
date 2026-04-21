using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using CineTrack.App.Common.Settings;
using CineTrack.App.Interfaces;
using CineTrack.Domain.Entities;

namespace CineTrack.App.Services;

public class EmailConfirmationService(
    IMailSender mailSender, 
    IOptions<WebSiteSettings> options, 
    ILogger<EmailConfirmationService> logger)
{
    private const int TokenExpirationHours = 1;
    private const string EmailSubject = "CineTrack: Confirm registration";
    private const string EmailBodyTemplate = """
                         Thank you for your registration to the CineTrack website.
                         To finish the registration, please click on the link below:
                         {0}
                         The link will expire in {1} hour(s).

                         Best regards,
                         CineTrack Team
                         """;
    
    public void GenerateConfirmationToken(User user)
    {
        var token = Guid.NewGuid();
        user.EmailConfirmationToken = token;
        user.EmailConfirmationTokenExpiresAt = DateTime.UtcNow.AddHours(TokenExpirationHours);
        user.IsEmailConfirmed = false;
    }

    public async Task SendConfirmationEmailAsync(User user)
    {
        var webSiteSettings = options.Value;
        var webSiteUrl = webSiteSettings.BaseUrl;
        var emailConfirmationPath = webSiteSettings.EmailConfirmationPath;
        
        var confirmationLink = $"{webSiteUrl}{emailConfirmationPath}?token={user.EmailConfirmationToken}";
        var emailBody = string.Format(EmailBodyTemplate, confirmationLink, TokenExpirationHours);
        
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