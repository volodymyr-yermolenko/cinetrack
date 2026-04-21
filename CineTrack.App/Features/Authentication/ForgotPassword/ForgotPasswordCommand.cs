using MediatR;
using CineTrack.App.Models.Authentication;

namespace CineTrack.App.Features.Authentication.ForgotPassword;

public class ForgotPasswordCommand(ForgotPasswordDto forgotPasswordData) : IRequest<Unit>
{
    public ForgotPasswordDto ForgotPasswordData { get; } = forgotPasswordData;
}