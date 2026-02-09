namespace CineTrack.Domain.Entities;

public class Genre : BaseIdEntity
{
    public string Name { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }    
}