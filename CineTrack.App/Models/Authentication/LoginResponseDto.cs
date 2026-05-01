namespace CineTrack.App.Models.Authentication;

public class LoginResponseDto
{
    public LoginStatus Status { get; set; }
    public string? AccessToken { get; set; }
}