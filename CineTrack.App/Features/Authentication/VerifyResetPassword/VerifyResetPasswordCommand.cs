using MediatR;
using CineTrack.App.Models.Authentication;

namespace CineTrack.App.Features.Authentication.VerifyResetPassword;

public class VerifyResetPasswordCommand(VerifyResetPasswordDto verifyResetPasswordData) 
    : IRequest<VerifyResetPasswordResponseDto>
{
    public VerifyResetPasswordDto VerifyResetPasswordData { get; } = verifyResetPasswordData;
}