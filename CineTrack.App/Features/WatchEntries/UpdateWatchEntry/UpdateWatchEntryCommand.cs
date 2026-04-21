using MediatR;
using CineTrack.App.Models.WatchEntries;

namespace CineTrack.App.Features.WatchEntries.UpdateWatchEntry;

public class UpdateWatchEntryCommand(int userId, int watchEntryId, UpdateWatchEntryDto watchEntry) : IRequest<Unit>
{
    public int UserId { get; } = userId;
    public int WatchEntryId { get; } = watchEntryId;
    public UpdateWatchEntryDto WatchEntry { get; } = watchEntry;
}