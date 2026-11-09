namespace Catalog.Domain.Entities;

public class PlaylistTrack : BaseEntity
{
    public Guid PlaylistId { get; set; }
    public Playlist Playlist { get; set; } = default!;
    public Guid TrackId { get; set; }
    public Track Track { get; set; } = default!;
    public DateTime AddedAt { get; set; } = DateTime.UtcNow;
    public int Position { get; set; }
}
