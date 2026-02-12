using CineTrack.Domain.Enums;

namespace CineTrack.App.Interfaces;

public interface IWatchEntryAttributes
{
    int MovieId { get; }
    int Rating { get; }
    ViewingContextType ViewingContext { get; }
    DateTime WatchedAt { get; }
    string? Mood { get; }
}