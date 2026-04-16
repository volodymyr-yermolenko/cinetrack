using Microsoft.Extensions.Logging;
using AutoMapper;
using MediatR;
using CineTrack.App.Common.Exceptions;
using CineTrack.App.Common.Helpers;
using CineTrack.App.Interfaces;
using CineTrack.App.Models.Users;
using CineTrack.Domain.Entities;

namespace CineTrack.App.Features.Users.RegisterUser;

public class RegisterUserCommandHandler(
    IMapper mapper, 
    IUserRepository userRepository,
    IMailSender mailSender,
    ILogger<RegisterUserCommandHandler> logger) 
    : IRequestHandler<RegisterUserCommand, RegistrationResult>
{
    public async Task<RegistrationResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var registrationData = request.RegistrationData;
        ValidateRegistrationData(registrationData);
        
        var existingUser = await userRepository.GetByEmailAsync(registrationData.Email);
        if (existingUser != null)
        {
            return existingUser.IsEmailConfirmed 
                ? RegistrationResult.UserExists 
                : RegistrationResult.UserNotConfirmed;  
        }
        
        var user = mapper.Map<User>(registrationData);
        user.PasswordHash = PasswordHasher.HashPassword(registrationData.Password);
        var now = DateTime.UtcNow;
        user.CreatedAt = now;
        user.UpdatedAt = now;
        
        GenerateConfirmationToken(user);

        await userRepository.AddUserAsync(user);
        await userRepository.UnitOfWork.SaveChangesAsync();

        await SendEmailConfirmationAsync(user);
        
        return RegistrationResult.Success;
    }

    private static void ValidateRegistrationData(UserRegistrationDto registrationData)
    {
        if (string.IsNullOrWhiteSpace(registrationData.Name))
        {
            throw new AppValidationException("Name is required");
        }
        ValidationHelper.ValidateEmail(registrationData.Email);
        ValidationHelper.ValidatePassword(registrationData.Password);
    }

    private static void GenerateConfirmationToken(User user)
    {
        var confirmationToken = Guid.NewGuid();
        user.EmailConfirmationToken = confirmationToken;
        user.EmailConfirmationTokenExpiresAt = DateTime.UtcNow.AddHours(1);
        user.IsEmailConfirmed = false;
    }

    private async Task SendEmailConfirmationAsync(User user)
    {
        const string emailSubject = "CineTrack: Confirm registration";

        //var webSiteUrl = configuration.GetValue<string>("WebSiteUrl");
        var confirmationLink = $"https://cinetrack-ui.vercel.app/confirm-email?token={user.EmailConfirmationToken}";
        var emailBody = $"""
                         Thank you for your registration to the CineTrack website.
                         To finish the registration, please click on the link below:
                         {confirmationLink}
                         
                         Best regards,
                         CineTrack Team
                         """;
        try
        {
            await mailSender.SendEmailAsync(user.Email, emailSubject, emailBody);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error sending email");
        }
    }
}
