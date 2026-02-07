using CineTrack.App.Interfaces;
using CineTrack.Domain.Entities;
using CineTrack.Domain.Interfaces;
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

    public Task<Movie?> GetMovieAsync(int movieId)
    {
        return context.Movies.FirstOrDefaultAsync(m => m.Id == movieId);
    }

    public async Task AddMovieAsync(Movie movie)
    {
        await context.Movies.AddAsync(movie);
    }

    public void UpdateMovie(Movie movie)
    {
        throw new NotImplementedException();
    }

    public void DeleteMovie(Movie movie)
    {
        throw new NotImplementedException();
    }
}