using CineTrack.Domain.Enums;

namespace CineTrack.App.Models;

public class MovieDto
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public int ReleaseYear { get; set; }
    public MovieType MovieType { get; set; }
    public string? ImageUrl { get; set; }
    public int UserId { get; set; }
}