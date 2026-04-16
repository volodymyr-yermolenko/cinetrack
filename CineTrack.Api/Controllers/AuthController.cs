using Microsoft.AspNetCore.Mvc;
using MediatR;
using CineTrack.App.Features.Users.ConfirmEmail;
using CineTrack.App.Features.Users.RegisterUser;
using CineTrack.App.Models.Users;

namespace CineTrack.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost("register")]
    [ProducesResponseType(typeof(RegistrationResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> Register([FromBody] UserRegistrationDto registrationData)
    {
        var command = new RegisterUserCommand(registrationData);
        var result = await mediator.Send(command);

        return Ok(result);
    }

    [HttpPost("confirm-email")]
    [ProducesResponseType(typeof(EmailConfirmationResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> ConfirmEmail([FromBody] Guid emailConfirmationToken)
    {
        var command = new ConfirmEmailCommand(emailConfirmationToken);
        var result = await mediator.Send(command);

        return Ok(result);
    }
    
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto login)
    {
        if (login is { Email: "test", Password: "test" })
        {
            //var accessToken = tokenService.GenerateAccessToken(login.Email);
            var accessToken = "";
            return Ok(new { AccessToken = accessToken });
        }

        return Unauthorized("Invalid credentials");
    }
}