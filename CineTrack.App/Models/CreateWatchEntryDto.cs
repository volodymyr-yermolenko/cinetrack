using CineTrack.App.Interfaces;
using CineTrack.Domain.Enums;

namespace CineTrack.App.Models;

public class CreateWatchEntryDto : IWatchEntryAttributes
{
    public int MovieId { get; init; }
    public int Rating { get; init; }
    public ViewingContextType ViewingContext { get; init; }
    public DateTime WatchedDate { get; init; }
    public string? Review { get; init; }
}