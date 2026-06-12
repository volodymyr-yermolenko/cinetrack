using MediatR;
using CineTrack.App.Models.Authentication;

namespace CineTrack.App.Features.Authentication.ResetPassword;

public class ResetPasswordCommand(ResetPasswordDto resetPasswordData) : IRequest<ResetPasswordResponseDto>
{
    public ResetPasswordDto ResetPasswordData { get; } = resetPasswordData;
}