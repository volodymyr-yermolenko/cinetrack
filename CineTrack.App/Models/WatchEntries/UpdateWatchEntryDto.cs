using CineTrack.App.Interfaces;
using CineTrack.Domain.Enums;

namespace CineTrack.App.Models.WatchEntries;

public class UpdateWatchEntryDto : IWatchEntryAttributes
{
    public int MovieId { get; init; }
    public int Rating { get; init; }
    public ViewingContext ViewingContext { get; init; }
    public DateTime WatchedDate { get; init; }
    public string? Review { get; init; }
}