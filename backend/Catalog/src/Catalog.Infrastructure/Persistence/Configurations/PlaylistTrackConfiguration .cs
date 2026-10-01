using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Persistence.Configurations;

internal class PlaylistTrackConfiguration : IEntityTypeConfiguration<PlaylistTrack>
{
    public void Configure(EntityTypeBuilder<PlaylistTrack> builder)
    {
        builder.HasOne(pt => pt.Track)
            .WithMany(t => t.PlaylistTracks)
            .HasForeignKey(t => t.TrackId);

        builder.HasOne(pt => pt.Playlist)
            .WithMany(p => p.PlaylistTracks)
            .HasForeignKey(p => p.PlaylistId);

        builder.HasIndex(x => new { x.PlaylistId, x.TrackId })
            .IsUnique();
    }
}
