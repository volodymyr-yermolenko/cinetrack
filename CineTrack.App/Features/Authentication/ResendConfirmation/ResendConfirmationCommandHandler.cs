using MediatR;
using CineTrack.App.Common.Helpers;
using CineTrack.App.Interfaces;
using CineTrack.App.Services;

namespace CineTrack.App.Features.Authentication.ResendConfirmation
{
    public class ResendConfirmationCommandHandler(
        IUserRepository userRepository, 
        EmailConfirmationService emailConfirmationService) 
        : IRequestHandler<ResendConfirmationCommand, Unit>
    {
        public async Task<Unit> Handle(ResendConfirmationCommand command, CancellationToken cancellationToken)
        {
            var email = command.ResendConfirmationData.Email;
            ValidationHelper.ValidateEmail(email);

            var user = await userRepository.GetByEmailAsync(email);
            if (user == null || user.IsEmailConfirmed) 
                return Unit.Value;
            
            emailConfirmationService.GenerateConfirmationToken(user);
            user.UpdatedAt = DateTime.UtcNow;
            
            userRepository.UpdateUser(user);
            await userRepository.UnitOfWork.SaveChangesAsync();
                
            await emailConfirmationService.SendConfirmationEmailAsync(user);
            return Unit.Value;
        }
    }
}