using CineTrack.Domain.Entities;

namespace CineTrack.App.Interfaces;

public interface IMovieRepository
{
    IUnitOfWork UnitOfWork { get; }
    Task<List<Movie>> GetMoviesAsync(int userId, int? genreId, string? searchString);
    Task<Movie?> GetMovieAsync(int userId, int movieId);
    Task AddMovieAsync(Movie movie);
    void UpdateMovie(Movie movie);
    void DeleteMovie(Movie movie);
    Task<bool> MovieExistsAsync(string title, int releaseYear, int userId, int exceptMovieId = 0);
}