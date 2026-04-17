using MediatR;
using CineTrack.App.Models.Authentication;

namespace CineTrack.App.Features.Authentication.ConfirmEmail;

public class ConfirmEmailCommand(Guid emailConfirmationToken) : IRequest<EmailConfirmationResult>
{
    public Guid EmailConfirmationToken { get; } = emailConfirmationToken;
}