using CineTrack.Domain.Enums;

namespace CineTrack.App.Interfaces;

public interface IMovieAttributes
{
    string Title { get; }
    int ReleaseYear { get; }
    MovieType MovieType { get; }
    string? ImageUrl { get; }
}