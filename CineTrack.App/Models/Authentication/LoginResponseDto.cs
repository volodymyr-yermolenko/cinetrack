namespace CineTrack.App.Models.Authentication;

public class LoginResponseDto
{
    public LoginResult Result { get; set; }
    public string? AccessToken { get; set; }
}