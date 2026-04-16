namespace CineTrack.App.Models;

public class UserRegistrationDto
{
    public required string Email { get; init; }
    public required string Name { get; init; }
    public required string Password { get; init; }
}