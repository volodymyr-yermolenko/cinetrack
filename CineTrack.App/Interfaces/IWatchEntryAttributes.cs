using CineTrack.Domain.Enums;

namespace CineTrack.App.Interfaces;

public interface IWatchEntryAttributes
{
    int MovieId { get; }
    int Rating { get; }
    ViewingContextType ViewingContext { get; }
    DateTime WatchedDate { get; }
    string? Review { get; }
}