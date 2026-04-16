using MediatR;
using CineTrack.App.Models.Users;

namespace CineTrack.App.Features.Users.RegisterUser;

public class RegisterUserCommand(UserRegistrationDto registrationData) : IRequest<RegistrationResult>
{
    public UserRegistrationDto RegistrationData { get; } = registrationData;
}