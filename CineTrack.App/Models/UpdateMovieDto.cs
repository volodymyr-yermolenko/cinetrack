using CineTrack.App.Interfaces;
using CineTrack.Domain.Enums;

namespace CineTrack.App.Models;

public class UpdateMovieDto : IMovieAttributes
{
    public required string Title { get; init; }
    public int ReleaseYear { get; init; }
    public MovieType MovieType { get; init; }
    public string? ImageUrl { get; init; }
    public required List<int> GenreIds { get; init; }    
}