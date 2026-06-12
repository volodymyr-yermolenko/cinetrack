using CineTrack.App.Interfaces;
using CineTrack.App.Models.Authentication;
using MediatR;

namespace CineTrack.App.Features.Authentication.VerifyResetPassword;

public class VerifyResetPasswordCommandHandler(IUserRepository userRepository)
    : IRequestHandler<VerifyResetPasswordCommand, VerifyResetPasswordResponseDto>
{
    public async Task<VerifyResetPasswordResponseDto> Handle(VerifyResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var result = new VerifyResetPasswordResponseDto();
        var token = request.VerifyResetPasswordData.ResetPasswordToken;
        var user = await userRepository.GetByPasswordResetTokenAsync(token);
        if (user == null)
        {
            result.TokenStatus = ResetPasswordTokenStatus.Invalid;
            return result;
        }
        
        if (user.PasswordResetTokenExpiresAt < DateTime.UtcNow)
        {
            result.TokenStatus = ResetPasswordTokenStatus.Expired;
            result.Email = user.Email;
            return result;
        }
        
        result.TokenStatus = ResetPasswordTokenStatus.Valid;
        return result;
    }
}