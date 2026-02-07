using CineTrack.Domain.Entities;
using CineTrack.Domain.Interfaces;

namespace CineTrack.App.Interfaces;

public interface IMovieRepository
{
    IUnitOfWork UnitOfWork { get; }
    Task<List<Movie>> GetMoviesAsync(int userId);
    Task<Movie?> GetMovieAsync(int movieId);
    Task AddMovieAsync(Movie movie);
    void UpdateMovie(Movie movie);
    void DeleteMovie(Movie movie);
}