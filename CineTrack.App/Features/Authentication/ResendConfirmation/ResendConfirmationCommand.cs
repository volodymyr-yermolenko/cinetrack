using MediatR;
using CineTrack.App.Models.Authentication;

namespace CineTrack.App.Features.Authentication.ResendConfirmation;

public class ResendConfirmationCommand(ResendConfirmationDto resendConfirmationData) : IRequest<Unit>
{
    public ResendConfirmationDto ResendConfirmationData { get; } = resendConfirmationData;
}