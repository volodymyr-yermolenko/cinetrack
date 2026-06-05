using Microsoft.AspNetCore.Mvc;
using MediatR;
using CineTrack.App.Features.Authentication.ConfirmEmail;
using CineTrack.App.Features.Authentication.ForgotPassword;
using CineTrack.App.Features.Authentication.GetCurrentUser;
using CineTrack.App.Features.Authentication.LoginUser;
using CineTrack.App.Features.Authentication.RegisterUser;
using CineTrack.App.Features.Authentication.ResendConfirmation;
using CineTrack.App.Features.Authentication.ResetPassword;
using CineTrack.App.Models.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace CineTrack.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(IMediator mediator) : BaseController
{
    [HttpPost("register")]
    [ProducesResponseType(typeof(RegistrationResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Register([FromBody] RegistrationDto registrationData)
    {
        var command = new RegisterUserCommand(registrationData);
        var result = await mediator.Send(command);

        return Ok(result);
    }

    [HttpPost("confirm-email")]
    [ProducesResponseType(typeof(EmailConfirmationResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> ConfirmEmail([FromBody] EmailConfirmationDto emailConfirmationData)
    {
        var command = new ConfirmEmailCommand(emailConfirmationData);
        var result = await mediator.Send(command);

        return Ok(result);
    }
    
    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Login([FromBody] LoginDto loginData)
    {
        var command = new LoginUserCommand(loginData);
        var result = await mediator.Send(command);
        
        return Ok(result);
    }
    
    [HttpPost("resend-confirmation")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> ResendConfirmation([FromBody] ResendConfirmationDto resendConfirmationData)
    {
        var command = new ResendConfirmationCommand(resendConfirmationData);
        await mediator.Send(command);

        return NoContent();
    }
    
    [HttpPost("forgot-password")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordData)
    {
        var command = new ForgotPasswordCommand(forgotPasswordData);
        await mediator.Send(command);

        return NoContent();
    }
    
    [HttpPost("reset-password")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordData)
    {
        var command = new ResetPasswordCommand(resetPasswordData);
        await mediator.Send(command);

        return NoContent();
    }
    
    [HttpGet("current-user")]
    [Authorize]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCurrentUser()
    {
        var request = new GetCurrentUserRequest(UserId);
        var result = await mediator.Send(request);
        
        return Ok(result);
    }
    
}