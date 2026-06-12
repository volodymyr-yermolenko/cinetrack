namespace CineTrack.App.Models.Authentication;

public class VerifyResetPasswordDto
{
    public required Guid ResetPasswordToken { get; init; }
}