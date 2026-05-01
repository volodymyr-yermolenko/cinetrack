using MediatR;
using CineTrack.App.Common.Constants;
using CineTrack.App.Common.Exceptions;
using CineTrack.App.Common.Helpers;
using CineTrack.App.Interfaces;
using CineTrack.App.Models.Authentication;

namespace CineTrack.App.Features.Authentication.LoginUser;

public class LoginUserCommandHandler(
    IUserRepository userRepository,
    ITokenService tokenService) 
    : IRequestHandler<LoginUserCommand, LoginResponseDto>
{
    public async Task<LoginResponseDto> Handle(LoginUserCommand command, CancellationToken cancellationToken)
    {
        var result = new LoginResponseDto();
        var loginData = command.LoginData;
        
        ValidationHelper.ValidateEmail(loginData.Email);
        if (string.IsNullOrWhiteSpace(loginData.Password))
        {
            throw new AppValidationException(AuthErrorMessages.PasswordRequired);
        }

        var user = await userRepository.GetByEmailAsync(loginData.Email);
        if (user == null || !PasswordHelper.VerifyHashedPassword(loginData.Password, user.PasswordHash))
        {
            result.Status = LoginStatus.InvalidCredentials;
            return result;
        }

        if (!user.IsEmailConfirmed)
        {
            result.Status = LoginStatus.EmailNotConfirmed;
            return result;
        }
        
        result.Status = LoginStatus.Success;
        result.AccessToken = tokenService.GenerateAccessToken(user);
        return result;
    }
}