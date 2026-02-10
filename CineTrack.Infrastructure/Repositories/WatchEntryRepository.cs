using CineTrack.App.Interfaces;
using CineTrack.Domain.Entities;
using CineTrack.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CineTrack.Infrastructure.Repositories;

public class WatchEntryRepository(AppDbContext context) : IWatchEntryRepository
{
    public IUnitOfWork UnitOfWork => context;
    
    public Task<List<WatchEntry>> GetWatchEntriesAsync(int userId)
    {
        return context.WatchEntries
            .Include(w => w.Movie)
            .Where(e => e.UserId == userId)
            .OrderByDescending(e => e.WatchedAt)
            .ToListAsync();
    }

    public Task<WatchEntry?> GetWatchEntryAsync(int userId, int watchEntryId)
    {
        return context.WatchEntries
            .Include(w => w.Movie)
            .FirstOrDefaultAsync(e => e.UserId == userId && e.Id == watchEntryId);
    }

    public async Task AddWatchEntryAsync(WatchEntry watchEntry)
    {
        await context.WatchEntries.AddAsync(watchEntry);
    }

    public void UpdateWatchEntry(WatchEntry watchEntry)
    {
        context.WatchEntries.Update(watchEntry);
    }

    public void DeleteWatchEntry(WatchEntry watchEntry)
    {
        context.WatchEntries.Remove(watchEntry);
    }

    public Task<bool> WatchEntryExistsAsync(int userId, int movieId, DateTime watchedAt, int exceptWatchEntryId = 0)
    {
        return context.WatchEntries.AnyAsync(w => w.UserId == userId && w.MovieId == movieId 
            && w.WatchedAt == watchedAt && w.Id != exceptWatchEntryId);
    }
}