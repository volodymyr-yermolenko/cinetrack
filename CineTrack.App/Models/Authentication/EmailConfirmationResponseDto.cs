namespace CineTrack.App.Models.Authentication;

public class EmailConfirmationResponseDto
{
    public EmailConfirmationResult Result { get; set; }
    public string? AccessToken { get; set; }
    public string? Email { get; set; }
}