namespace CineTrack.App.Models.Authentication;

public class VerifyResetPasswordResponseDto
{
    public ResetPasswordTokenStatus TokenStatus { get; set; }
    public string? Email { get; set; }
}