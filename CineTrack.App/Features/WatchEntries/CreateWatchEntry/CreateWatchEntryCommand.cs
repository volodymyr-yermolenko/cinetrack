using CineTrack.App.Models;
using MediatR;

namespace CineTrack.App.Features.WatchEntries.CreateWatchEntry;

public class CreateWatchEntryCommand(int userId) : IRequest<int>
{
    public int UserId { get; } = userId;
    public required CreateWatchEntryDto WatchEntry { get; init; }
}