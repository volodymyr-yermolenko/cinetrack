using CineTrack.App.Models;
using MediatR;

namespace CineTrack.App.Features.WatchEntries.UpdateWatchEntry;

public class UpdateWatchEntryCommand(int userId) : IRequest<Unit>
{
    public int UserId { get; } = userId;
    public required int WatchEntryId { get; init; }
    public required UpdateWatchEntryDto WatchEntry { get; init; }
}