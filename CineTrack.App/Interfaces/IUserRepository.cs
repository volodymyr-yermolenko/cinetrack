using CineTrack.Domain.Entities;

namespace CineTrack.App.Interfaces;

public interface IUserRepository
{
    IUnitOfWork UnitOfWork { get; }
    Task AddUserAsync(User user);
    void UpdateUser(User user);
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByEmailConfirmationTokenAsync(Guid userId);
    Task<User?> GetByPasswordResetTokenAsync(Guid userId);
}