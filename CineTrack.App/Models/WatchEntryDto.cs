using CineTrack.Domain.Enums;

namespace CineTrack.App.Models;

public class WatchEntryDto
{
    public int Id { get; init; }
    public int Rating { get; init; }
    public ViewingContext ViewingContext { get; init; }
    public DateTime WatchedDate { get; init; }
    public string? Review { get; init; }
    public MovieDto Movie { get; init; } = null!;    
}