using CineTrack.Domain.Enums;

namespace CineTrack.Domain.Entities;

public class WatchEntry : BaseIdEntity
{
    public int UserId { get; set; }
    public int MovieId { get; set; }
    public int Rating { get; set; }
    public ViewingContext ViewingContext { get; set; }
    public DateTime WatchedDate { get; set; }
    public string? Review { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public Movie Movie { get; set; } = null!;
}