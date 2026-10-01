using Catalog.Application.Interfaces;
using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Persistence;

internal class ApplicationDbContextInitialiser(ApplicationDbContext _context)
    : IApplicationDbContextInitialiser
{
    public async Task InitialiseAsync(CancellationToken ct = default)
    {
        if (_context.Database.IsNpgsql())
        {
            await _context.Database.MigrateAsync(ct);
        }
    }

    public async Task SeedAsync(CancellationToken ct = default)
    {
        if (await _context.Tracks.AnyAsync(ct))
        {
            return;
        }

        var track1 = new Track
        {
            Name = "Bohemian Rhapsody",
            Author = "Queen",
            Album = "A Night at the Opera",
            Rating = 5,
            HasFile = false
        };

        var track2 = new Track
        {
            Name = "Billie Jean",
            Author = "Michael Jackson",
            Album = "Thriller",
            Rating = 5,
            HasFile = false
        };

        var track3 = new Track
        {
            Name = "Stairway to Heaven",
            Author = "Led Zeppelin",
            Album = "Led Zeppelin IV",
            Rating = 5,
            HasFile = false
        };

        var playlist = new Playlist
        {
            Name = "Favorites",
            PlaylistTracks =
            [
                new PlaylistTrack
                {
                    Track = track1,
                    Position = 1
                },
                new PlaylistTrack
                {
                    Track = track2,
                    Position = 2
                }
            ]
        };

        _context.Tracks.Add(track3);
        _context.Playlists.Add(playlist);

        await _context.SaveChangesAsync(ct);
    }
}
