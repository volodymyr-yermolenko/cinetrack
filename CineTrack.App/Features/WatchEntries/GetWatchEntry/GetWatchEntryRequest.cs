using MediatR;
using CineTrack.App.Models.WatchEntries;

namespace CineTrack.App.Features.WatchEntries.GetWatchEntry;

public class GetWatchEntryRequest(int userId, int watchEntryId) : IRequest<WatchEntryDto>
{
    public int UserId { get; } = userId;
    public int WatchEntryId { get; } = watchEntryId;
}