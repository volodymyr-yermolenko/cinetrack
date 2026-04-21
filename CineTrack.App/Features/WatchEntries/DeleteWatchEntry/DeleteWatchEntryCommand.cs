using MediatR;

namespace CineTrack.App.Features.WatchEntries.DeleteWatchEntry;

public class DeleteWatchEntryCommand(int userId, int watchEntryId) : IRequest<Unit>
{
    public int UserId { get; } = userId;
    public int WatchEntryId { get; } = watchEntryId;
}