using CineTrack.App.Interfaces;
using CineTrack.Domain.Entities;
using CineTrack.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CineTrack.Infrastructure.Repositories;

public class GenreRepository(AppDbContext context) : IGenreRepository
{
    public Task<List<Genre>> GetGenresAsync()
    {
        return context.Genres.OrderBy(g => g.Name).ToListAsync();
    }

    public Task<List<Genre>> GetGenresByIdsAsync(List<int> genreIds)
    {
        return context.Genres.Where(g => genreIds.Contains(g.Id)).ToListAsync();
    }

    public async Task<bool> AllGenresExistAsync(List<int> genreIds)
    {
        var existingCount = await context.Genres.CountAsync(g => genreIds.Contains(g.Id));
        return genreIds.Count == existingCount;
    }
}