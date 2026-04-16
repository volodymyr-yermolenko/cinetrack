using MediatR;
using CineTrack.App.Interfaces;
using CineTrack.App.Models;

namespace CineTrack.App.Features.Users.ConfirmEmail;

public class ConfirmEmailCommandHandler(IUserRepository userRepository) : IRequestHandler<ConfirmEmailCommand, EmailConfirmationResult>
{
    public async Task<EmailConfirmationResult> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByEmailConfirmationTokenAsync(request.EmailConfirmationToken);
        if (user == null)
        {
            return EmailConfirmationResult.UserNotFound;
        }
        
        if (user.EmailConfirmationTokenExpiresAt < DateTime.UtcNow)
        {
            return EmailConfirmationResult.TokenExpired;
        }
        
        user.IsEmailConfirmed = true;
        user.EmailConfirmationToken = null;
        user.EmailConfirmationTokenExpiresAt = null;
        
        userRepository.UpdateUser(user);
        await userRepository.UnitOfWork.SaveChangesAsync();
        
        return EmailConfirmationResult.Success;
    }
}