using Catalog.Application.Interfaces;
using Catalog.Application.Tracks.DTOs;
using Catalog.Domain.Entities;
using MediatR;

namespace Catalog.Application.Tracks.Commands;

public record CreateTrackCommand(CreateTrackDto dto) : IRequest<TrackDto>;
internal class CreateTrackcommandHandler(IApplicationDbContext _context) : IRequestHandler<CreateTrackCommand, TrackDto>
{
    public async Task<TrackDto> Handle(CreateTrackCommand request, CancellationToken ct)
    {
        var track = new Track 
        {
            Id = Guid.NewGuid(),
            Name = request.dto.Name,
            Author = request.dto.Author,
            Album = request.dto.Album,
            Rating = request.dto.Rating,
            HasFile = false,
            CreatedAt = DateTime.UtcNow
        };

        _context.Tracks.Add(track);
        await _context.SaveChangesAsync();

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
