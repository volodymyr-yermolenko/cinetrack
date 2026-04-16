namespace CineTrack.App.Models;

public class UserLoginDto
{
    public required string Email { get; init; }
    public required string Password { get; init; }    
}