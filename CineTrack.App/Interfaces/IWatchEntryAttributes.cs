using CineTrack.Domain.Enums;

namespace CineTrack.App.Interfaces;

public interface IWatchEntryAttributes
{
    int MovieId { get; }
    int Rating { get; }
    ViewingContext ViewingContext { get; }
    DateTime WatchedDate { get; }
    string? Review { get; }
}