namespace CineTrack.App.Models.Authentication;

public class RegistrationDto
{
    public required string Email { get; init; }
    public required string Name { get; init; }
    public required string Password { get; init; }
}