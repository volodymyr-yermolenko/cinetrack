using MediatR;
using CineTrack.App.Models.Authentication;

namespace CineTrack.App.Features.Authentication.RegisterUser;

public class RegisterUserCommand(RegistrationDto registrationData) : IRequest<RegistrationResponseDto>
{
    public RegistrationDto RegistrationData { get; } = registrationData;
}