namespace Catalog.Domain.Entities;

public class Playlist : BaseEntity
{
    public string Name { get; set; } = default!;
    public ICollection<PlaylistTrack> PlaylistTracks { get; set; } = [];
}
