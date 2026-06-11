using MediatR;
using CineTrack.App.Common.Helpers;
using CineTrack.App.Interfaces;

namespace CineTrack.App.Features.Authentication.ForgotPassword;

public class ForgotPasswordCommandHandler(
    IUserRepository userRepository, 
    IPasswordResetService passwordResetService) 
    : IRequestHandler<ForgotPasswordCommand, Unit>
{
    public async Task<Unit> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        var email = request.ForgotPasswordData.Email;
        ValidationHelper.ValidateEmail(email);

        var user = await userRepository.GetByEmailAsync(email);
        if (user == null) 
            return Unit.Value;
        
        passwordResetService.GenerateResetToken(user);
        user.UpdatedAt = DateTime.UtcNow;
        
        userRepository.UpdateUser(user);
        await userRepository.UnitOfWork.SaveChangesAsync();
        
        await passwordResetService.SendResetEmailAsync(user);
        
        return Unit.Value;
    }
}