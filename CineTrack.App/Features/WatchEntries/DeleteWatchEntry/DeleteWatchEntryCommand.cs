using MediatR;

namespace CineTrack.App.Features.WatchEntries.DeleteWatchEntry;

public class DeleteWatchEntryCommand(int userId) : IRequest<Unit>
{
    public int UserId { get; } = userId;
    public required int WatchEntryId { get; init; }
}