using CineTrack.Domain.Entities;

namespace CineTrack.App.Interfaces;

public interface IUserRepository
{
    IUnitOfWork UnitOfWork { get; }
    Task AddUserAsync(User user);
    void UpdateUser(User user);
    Task<User?> GetByIdAsync(int userId);
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByEmailConfirmationTokenAsync(Guid token);
    Task<User?> GetByPasswordResetTokenAsync(Guid token);
}