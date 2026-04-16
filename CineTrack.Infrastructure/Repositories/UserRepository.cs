using Microsoft.EntityFrameworkCore;
using CineTrack.App.Interfaces;
using CineTrack.Domain.Entities;
using CineTrack.Infrastructure.Persistence;

namespace CineTrack.Infrastructure.Repositories;

public class UserRepository(AppDbContext context) : IUserRepository
{
    public IUnitOfWork UnitOfWork => context;
    
    public async Task AddUserAsync(User user)
    {
        await context.Users.AddAsync(user);
    }

    public void UpdateUser(User user)
    {
        context.Users.Update(user); 
    }

    public Task<User?> GetByEmailAsync(string email)
    {
        return context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public Task<User?> GetByEmailConfirmationTokenAsync(Guid token)
    {
        return context.Users.FirstOrDefaultAsync(u => u.EmailConfirmationToken == token);
    }

    public Task<User?> GetByPasswordResetTokenAsync(Guid token)
    {
        return context.Users.FirstOrDefaultAsync(u => u.PasswordResetToken == token);
    }
}