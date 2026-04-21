namespace CineTrack.App.Models.Authentication;

public class ResetPasswordDto
{
    public required Guid ResetPasswordToken { get; init; }
    public required string NewPassword { get; init; }
}