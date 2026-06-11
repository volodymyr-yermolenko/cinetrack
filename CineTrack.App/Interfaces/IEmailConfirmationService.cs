using CineTrack.Domain.Entities;

namespace CineTrack.App.Interfaces;

public interface IEmailConfirmationService
{
    void GenerateConfirmationToken(User user);
    Task SendConfirmationEmailAsync(User user);
}