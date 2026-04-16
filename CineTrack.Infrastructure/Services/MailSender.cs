using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using CineTrack.App.Interfaces;
using CineTrack.Infrastructure.Settings;

namespace CineTrack.Infrastructure.Services;

public class MailSender(IOptions<EmailSettings> options) : IMailSender
{
    public async Task SendEmailAsync(string recipientEmail, string subject, string body)
    {
        var emailSettings = options.Value;
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("CineTrack Support Team", emailSettings.SenderEmail));
        message.To.Add(new MailboxAddress(null, recipientEmail));
        message.Subject = subject;
        message.Body = new TextPart("plain")
        {
            Text = body
        };

        using var client = new SmtpClient();
        try
        {
            await client.ConnectAsync(emailSettings.SmtpServer, emailSettings.SmtpPort, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(emailSettings.SenderEmail, emailSettings.AppPassword);
            
            await client.SendAsync(message);
        }
        finally
        {
            await client.DisconnectAsync(true);
        }
    }
}