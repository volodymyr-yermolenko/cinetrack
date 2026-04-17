using MediatR;
using CineTrack.App.Models.Authentication;

namespace CineTrack.App.Features.Authentication.LoginUser;

public class LoginUserCommand(LoginDto loginData) : IRequest<LoginResponseDto>
{
    public LoginDto LoginData { get; } = loginData;
}