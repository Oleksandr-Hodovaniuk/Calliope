using Catalog.Application.Interfaces;
using Catalog.Application.Tracks.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Application.Tracks.Commands;

public record UpdateTrackCommand(Guid id, UpdateTrackDto dto) : IRequest<TrackDto>;
internal class UpdateTrackCommandHandler(IApplicationDbContext _context) : IRequestHandler<UpdateTrackCommand, TrackDto>
{
    public async Task<TrackDto> Handle(UpdateTrackCommand request, CancellationToken ct)
    {
        var track = await _context.Tracks.FirstOrDefaultAsync(t => t.Id == request.id, ct);

        if (track == null)
        {
            throw new KeyNotFoundException($"Track with id '{request.id}' was not found.");
        }

        track.Name = request.dto.Name;
        track.Author = request.dto.Author;
        track.Album = request.dto.Album;
        track.Rating = request.dto.Rating;

        await _context.SaveChangesAsync(ct);

        return new TrackDto(
            track.Id,
            track.Name,
            track.Author,
            track.Album,
            track.Rating,
            track.HasFile,
            track.CreatedAt
        );
    }
}
