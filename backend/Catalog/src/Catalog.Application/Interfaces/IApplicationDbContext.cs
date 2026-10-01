using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Application.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Track> Tracks { get; }
    DbSet<Playlist> Playlists { get; }
    DbSet<PlaylistTrack> PlaylistTracks { get; }
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}
