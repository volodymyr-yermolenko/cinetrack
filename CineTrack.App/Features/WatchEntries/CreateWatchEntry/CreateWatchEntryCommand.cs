using MediatR;
using CineTrack.App.Models.WatchEntries;

namespace CineTrack.App.Features.WatchEntries.CreateWatchEntry;

public class CreateWatchEntryCommand(int userId, CreateWatchEntryDto watchEntry) : IRequest<int>
{
    public int UserId { get; } = userId;
    public CreateWatchEntryDto WatchEntry { get; } = watchEntry;
}