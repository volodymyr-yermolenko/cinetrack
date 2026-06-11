using CineTrack.Domain.Entities;

namespace CineTrack.App.Interfaces;

public interface IPasswordResetService
{
    void GenerateResetToken(User user);
    Task SendResetEmailAsync(User user);
}