using Catalog.Application.Interfaces;
using Catalog.Application.Tracks.Dtoes;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Application.Tracks.Commands;

public record UpdateTrackCommand(Guid Id, UpdateTrackDto Dto) : IRequest<TrackDto>;
internal class UpdateTrackCommandHandler(IApplicationDbContext _context) : IRequestHandler<UpdateTrackCommand, TrackDto>
{
    public async Task<TrackDto> Handle(UpdateTrackCommand request, CancellationToken ct)
    {
        var track = await _context.Tracks
            .FirstOrDefaultAsync(t => t.Id == request.Id, ct)
            ?? throw new KeyNotFoundException($"Track with id '{request.Id}' was not found."); ;

        track.Name = request.Dto.Name;
        track.Author = request.Dto.Author;
        track.Album = request.Dto.Album;
        track.Rating = request.Dto.Rating;
        track.FileId = request.Dto.FileId;

        await _context.SaveChangesAsync(ct);

        return new TrackDto
        (
            track.Id,
            track.Name,
            track.Author,
            track.Album,
            track.Rating,
            null,
            track.CreatedAt
        );
    }
}
