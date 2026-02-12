using CineTrack.Domain.Entities;

namespace CineTrack.App.Interfaces;

public interface IGenreRepository
{
    Task<List<Genre>> GetGenresAsync();
    Task<List<Genre>> GetGenresByIdsAsync(List<int> genreIds);
    Task<bool> AllGenresExistAsync(List<int> genreIds);
}