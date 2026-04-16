using CineTrack.App.Models.Genres;
using CineTrack.Domain.Enums;

namespace CineTrack.App.Models.Movies;

public class MovieDto
{
    public int Id { get; init; }
    public required string Title { get; init; }
    public int ReleaseYear { get; init; }
    public MovieType MovieType { get; init; }
    public string? ImageUrl { get; init; }
    public List<GenreDto> Genres { get; init; } = [];
}