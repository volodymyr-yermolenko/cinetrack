using CineTrack.Domain.Entities;

namespace CineTrack.App.Interfaces;

public interface IGenreRepository
{
    public Task<List<Genre>> GetGenresAsync();
}