namespace CineTrack.App.Models.Authentication;

public class EmailConfirmationDto
{
    public required Guid EmailConfirmationToken  { get; init; } 
}