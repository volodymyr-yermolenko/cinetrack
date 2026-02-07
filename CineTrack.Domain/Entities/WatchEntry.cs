using CineTrack.Domain.Enums;

namespace CineTrack.Domain.Entities;

public class WatchEntry : BaseIdEntity
{
    public int UserId { get; set; }
    public int MovieId { get; set; }
    public int Rating { get; set; }
    public WatchedWithType WatchedWith { get; set; }
    public DateTime WatchedAt { get; set; }
    public string? Mood { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}