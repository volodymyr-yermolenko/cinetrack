using MediatR;
using CineTrack.App.Models.Users;

namespace CineTrack.App.Features.Users.ConfirmEmail;

public class ConfirmEmailCommand(Guid emailConfirmationToken) : IRequest<EmailConfirmationResult>
{
    public Guid EmailConfirmationToken { get; } = emailConfirmationToken;
}