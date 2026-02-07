namespace CineTrack.Domain.Entities;

public class Genre : BaseIdEntity
{
    public required string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }    
}