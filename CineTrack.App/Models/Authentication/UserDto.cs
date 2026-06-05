namespace CineTrack.App.Models.Authentication;

public class UserDto
{
    public int Id { get; init; }
    public required string Email { get; init; }
    public required string Name { get; init; }
}