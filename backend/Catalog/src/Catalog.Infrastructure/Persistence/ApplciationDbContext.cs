using Catalog.Application.Interfaces;
using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Catalog.Infrastructure.Persistence;

internal class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options),
    IApplicationDbContext
{
    public DbSet<Track> Tracks => Set<Track>();
    public DbSet<Playlist> Playlists => Set<Playlist>();
    public DbSet<PlaylistTrack> PlaylistTracks => Set<PlaylistTrack>();
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}
