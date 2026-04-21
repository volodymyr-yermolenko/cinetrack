namespace CineTrack.App.Models.Authentication;

public class RegistrationDto
{
    public required string Email { get; init; }
    public required string Password { get; init; }
    public string Name { get; set; } = string.Empty;
}