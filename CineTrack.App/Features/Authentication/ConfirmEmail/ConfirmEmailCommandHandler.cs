using MediatR;
using CineTrack.App.Interfaces;
using CineTrack.App.Models.Authentication;

namespace CineTrack.App.Features.Authentication.ConfirmEmail;

public class ConfirmEmailCommandHandler(
    IUserRepository userRepository, 
    ITokenService tokenService) 
    : IRequestHandler<ConfirmEmailCommand, EmailConfirmationResponseDto>
{
    public async Task<EmailConfirmationResponseDto> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var result = new EmailConfirmationResponseDto();
        var confirmationData = request.EmailConfirmationData;
        var user = await userRepository.GetByEmailConfirmationTokenAsync(confirmationData.EmailConfirmationToken);
        if (user == null)
        {
            result.Result = EmailConfirmationResult.InvalidToken;
            return result;
        }
        
        if (user.EmailConfirmationTokenExpiresAt < DateTime.UtcNow)
        {
            result.Result = EmailConfirmationResult.TokenExpired;
            result.Email = user.Email;
            return result;
        }
        
        user.IsEmailConfirmed = true;
        user.EmailConfirmationToken = null;
        user.EmailConfirmationTokenExpiresAt = null;
        user.UpdatedAt = DateTime.UtcNow;
        
        userRepository.UpdateUser(user);
        await userRepository.UnitOfWork.SaveChangesAsync();

        result.Result = EmailConfirmationResult.Success;
        result.AccessToken = tokenService.GenerateAccessToken(user);

        return result;
    }
}