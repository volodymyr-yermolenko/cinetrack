using MediatR;
using CineTrack.App.Models.Authentication;

namespace CineTrack.App.Features.Authentication.ConfirmEmail;

public class ConfirmEmailCommand(EmailConfirmationDto emailConfirmationData) 
    : IRequest<EmailConfirmationResponseDto>
{
    public EmailConfirmationDto EmailConfirmationData { get; } = emailConfirmationData;
}