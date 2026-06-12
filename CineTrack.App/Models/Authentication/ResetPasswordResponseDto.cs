namespace CineTrack.App.Models.Authentication;

public class ResetPasswordResponseDto
{
    public ResetPasswordStatus Status { get; set; }
    public string? AccessToken { get; set; }
    public string? Email { get; set; }
}