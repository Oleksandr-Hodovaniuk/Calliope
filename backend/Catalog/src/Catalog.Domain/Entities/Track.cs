namespace Catalog.Domain.Entities;

public class Track : BaseEntity
{
    public string Name { get; set; } = default!;
    public string? Author { get; set; }
    public string? Album { get; set; }
    public byte? Rating { get; set; }
    public bool HasFile { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<PlaylistTrack> PlaylistTracks { get; set; } = [];
}
