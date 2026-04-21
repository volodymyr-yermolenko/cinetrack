using MediatR;
using CineTrack.App.Models.Authentication;

namespace CineTrack.App.Features.Authentication.ResetPassword;

public class ResetPasswordCommand(ResetPasswordDto resetPasswordData) : IRequest<Unit>
{
    public ResetPasswordDto ResetPasswordData { get; } = resetPasswordData;
}