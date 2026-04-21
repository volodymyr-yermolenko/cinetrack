using CineTrack.App.Interfaces;
using CineTrack.Domain.Enums;

namespace CineTrack.App.Models.Movies;

public class CreateMovieDto : IMovieAttributes
{
    public string Title { get; set; } = string.Empty;
    public int ReleaseYear { get; init; }
    public MovieType MovieType { get; init; }
    public string? ImageUrl { get; init; }
    public required List<int> GenreIds { get; init; }
}