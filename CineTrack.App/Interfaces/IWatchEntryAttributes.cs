using CineTrack.Domain.Enums;

namespace CineTrack.App.Interfaces;

public interface IWatchEntryAttributes
{
    public int MovieId { get; }
    public int Rating { get; }
    public ViewingContextType ViewingContext { get; }
    public DateTime WatchedAt { get; }
    public string? Mood { get; }
}