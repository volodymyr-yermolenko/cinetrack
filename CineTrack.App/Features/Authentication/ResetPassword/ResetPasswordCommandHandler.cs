using MediatR;
using CineTrack.App.Common.Helpers;
using CineTrack.App.Interfaces;

namespace CineTrack.App.Features.Authentication.ResetPassword;

public class ResetPasswordCommandHandler(IUserRepository userRepository) 
    : IRequestHandler<ResetPasswordCommand, Unit>
{
    public async Task<Unit> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var resetPasswordData = request.ResetPasswordData;
        ValidationHelper.ValidatePassword(resetPasswordData.NewPassword);

        var user = await userRepository.GetByPasswordResetTokenAsync(resetPasswordData.ResetPasswordToken);
        if (user == null)
        {
            return Unit.Value;
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

        return Unit.Value;
    }
}