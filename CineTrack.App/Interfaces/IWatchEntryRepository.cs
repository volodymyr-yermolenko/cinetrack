using CineTrack.Domain.Entities;

namespace CineTrack.App.Interfaces;

public interface IWatchEntryRepository
{
    IUnitOfWork UnitOfWork { get; }
    Task<List<WatchEntry>> GetWatchEntriesAsync(int userId, int? genreId, string? searchString);
    Task<WatchEntry?> GetWatchEntryAsync(int userId, int watchEntryId);
    Task AddWatchEntryAsync(WatchEntry watchEntry);
    void UpdateWatchEntry(WatchEntry watchEntry);
    void DeleteWatchEntry(WatchEntry watchEntry);
    Task<bool> WatchEntryExistsAsync(int userId, int movieId, DateTime watchedAt, int exceptWatchEntryId = 0);
}