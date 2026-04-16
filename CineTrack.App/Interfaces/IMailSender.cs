namespace CineTrack.App.Interfaces;

public interface IMailSender
{
    Task SendEmailAsync(string recipientEmail, string subject, string body);
}