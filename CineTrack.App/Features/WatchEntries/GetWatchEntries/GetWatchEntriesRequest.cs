using CineTrack.App.Models;
using MediatR;

namespace CineTrack.App.Features.WatchEntries.GetWatchEntries;

public class GetWatchEntriesRequest(int userId) : IRequest<List<WatchEntryDto>>
{
    public int UserId { get; } = userId;
}