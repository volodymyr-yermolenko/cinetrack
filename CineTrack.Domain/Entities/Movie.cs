using CineTrack.Domain.Enums;

namespace CineTrack.Domain.Entities;

public class Movie : BaseIdEntity
{
    public required string Title { get; set; }
    public int ReleaseYear { get; set; }
    public MovieType MovieType { get; set; }
    public string? ImageUrl { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }    
}