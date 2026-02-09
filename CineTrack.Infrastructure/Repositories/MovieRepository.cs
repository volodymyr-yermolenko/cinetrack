using CineTrack.App.Interfaces;
using CineTrack.Domain.Entities;
using CineTrack.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CineTrack.Infrastructure.Repositories;

public class MovieRepository(AppDbContext context) : IMovieRepository
{
    public IUnitOfWork UnitOfWork => context;
    
    public Task<List<Movie>> GetMoviesAsync(int userId)
    {
        return context.Movies
            .Where(m => m.UserId == userId)
            .OrderBy(m => m.Title)
            .ToListAsync();
    }

    public Task<Movie?> GetMovieAsync(int userId, int movieId)
    {
        return context.Movies.FirstOrDefaultAsync(m => m.UserId == userId && m.Id == movieId);
    }

    public async Task AddMovieAsync(Movie movie)
    {
        await context.Movies.AddAsync(movie);
    }

    public void UpdateMovie(Movie movie)
    {
        context.Movies.Update(movie);
    }

    public void DeleteMovie(Movie movie)
    {
        context.Movies.Remove(movie);
    }
    
    public Task<bool> MovieExistsAsync(string title, int releaseYear, int userId, int exceptMovieId = 0)
    {
        return context.Movies.AnyAsync(m => m.Title == title.Trim() && m.ReleaseYear == releaseYear && m.UserId == userId && m.Id != exceptMovieId);
    }
}