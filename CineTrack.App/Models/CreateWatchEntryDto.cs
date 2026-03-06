using CineTrack.App.Interfaces;
using CineTrack.Domain.Enums;

namespace CineTrack.App.Models;

public class CreateWatchEntryDto : IWatchEntryAttributes
{
    public int MovieId { get; init; }
    public int Rating { get; init; }
    public ViewingContextType ViewingContext { get; init; }
    public DateTime WatchedAt { get; init; }
    public string? Impression { get; init; }
}