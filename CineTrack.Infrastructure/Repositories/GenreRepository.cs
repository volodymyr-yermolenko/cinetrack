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
}