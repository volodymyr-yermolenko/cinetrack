namespace CineTrack.App.Models.Authentication;

public class EmailConfirmationResponseDto
{
    public EmailConfirmationStatus Status { get; set; }
    public string? AccessToken { get; set; }
    public string? Email { get; set; }
}