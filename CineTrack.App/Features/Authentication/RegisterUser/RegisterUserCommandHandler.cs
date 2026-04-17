using AutoMapper;
using MediatR;
using CineTrack.App.Common.Exceptions;
using CineTrack.App.Common.Helpers;
using CineTrack.App.Interfaces;
using CineTrack.App.Models.Authentication;
using CineTrack.App.Services;
using CineTrack.Domain.Entities;

namespace CineTrack.App.Features.Authentication.RegisterUser;

public class RegisterUserCommandHandler(
    IMapper mapper, 
    IUserRepository userRepository,
    EmailConfirmationService emailConfirmationService)
    : IRequestHandler<RegisterUserCommand, RegistrationResponseDto>
{
    public async Task<RegistrationResponseDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var result = new RegistrationResponseDto();
        var registrationData = request.RegistrationData;
        ValidateRegistrationData(registrationData);
        
        var existingUser = await userRepository.GetByEmailAsync(registrationData.Email);
        if (existingUser != null)
        {
            result.Result = existingUser.IsEmailConfirmed 
                ? RegistrationResult.UserExists 
                : RegistrationResult.UserNotConfirmed;
            return result;
        }
        
        var user = mapper.Map<User>(registrationData);
        user.PasswordHash = PasswordHelper.HashPassword(registrationData.Password);
        var now = DateTime.UtcNow;
        user.CreatedAt = now;
        user.UpdatedAt = now;
        
        emailConfirmationService.GenerateEmailConfirmationToken(user);

        await userRepository.AddUserAsync(user);
        await userRepository.UnitOfWork.SaveChangesAsync();

        await emailConfirmationService.SendEmailConfirmationAsync(user);
        
        result.Result = RegistrationResult.Success;
        return result;
    }

    private static void ValidateRegistrationData(RegistrationDto registrationData)
    {
        if (string.IsNullOrWhiteSpace(registrationData.Name))
        {
            throw new AppValidationException("Name is required");
        }
        ValidationHelper.ValidateEmail(registrationData.Email);
        ValidationHelper.ValidatePassword(registrationData.Password);
    }
}
