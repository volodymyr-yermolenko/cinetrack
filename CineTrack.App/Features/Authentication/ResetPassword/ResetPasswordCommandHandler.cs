using MediatR;
using CineTrack.App.Common.Helpers;
using CineTrack.App.Interfaces;
using CineTrack.App.Models.Authentication;

namespace CineTrack.App.Features.Authentication.ResetPassword;

public class ResetPasswordCommandHandler(
    IUserRepository userRepository,
    ITokenService tokenService) 
    : IRequestHandler<ResetPasswordCommand, ResetPasswordResponseDto>
{
    public async Task<ResetPasswordResponseDto> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var result = new ResetPasswordResponseDto();
        
        var resetPasswordData = request.ResetPasswordData;
        ValidationHelper.ValidatePassword(resetPasswordData.NewPassword);

        var user = await userRepository.GetByPasswordResetTokenAsync(resetPasswordData.ResetPasswordToken);
        if (user == null)
        {
            result.Status = ResetPasswordStatus.InvalidToken;
            return result;
        }
        if (user.PasswordResetTokenExpiresAt < DateTime.UtcNow)
        {
            result.Status = ResetPasswordStatus.TokenExpired;
            result.Email = user.Email;
            return result;
        }

        user.PasswordHash = PasswordHelper.HashPassword(resetPasswordData.NewPassword);
        user.PasswordResetToken = null;
        user.PasswordResetTokenExpiresAt = null;

        if (!user.IsEmailConfirmed)
        {
            user.EmailConfirmationToken = null;
            user.EmailConfirmationTokenExpiresAt = null;
            user.IsEmailConfirmed = true;
        }
        user.UpdatedAt = DateTime.UtcNow;
        
        userRepository.UpdateUser(user);
        await userRepository.UnitOfWork.SaveChangesAsync();
        
        result.Status = ResetPasswordStatus.Success;
        result.AccessToken = tokenService.GenerateAccessToken(user);
        return result;
    }
}