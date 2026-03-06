using CineTrack.App.Interfaces;
using CineTrack.Domain.Entities;
using CineTrack.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CineTrack.Infrastructure.Repositories;

public class WatchEntryRepository(AppDbContext context) : IWatchEntryRepository
{
    public IUnitOfWork UnitOfWork => context;
    
    public Task<List<WatchEntry>> GetWatchEntriesAsync(int userId, int? genreId, string? searchString)
    {
        var query = context.WatchEntries
            .Include(w => w.Movie)
            .ThenInclude(m => m.Genres.OrderBy(g => g.Name))
            .Where(m => m.UserId == userId);

        if (genreId.HasValue)
        {
            query = query.Where(m => m.Movie.Genres.Any(g => g.Id == genreId.Value));
        }

        if (!string.IsNullOrEmpty(searchString))
        {
            query = query.Where(m => m.Movie.Title.Contains(searchString.Trim()));
        }
        
        return query
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