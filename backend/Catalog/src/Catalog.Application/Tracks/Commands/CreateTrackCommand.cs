using Catalog.Application.Interfaces;
using Catalog.Application.Tracks.Dtoes;
using Catalog.Domain.Entities;
using MediatR;

namespace Catalog.Application.Tracks.Commands;

public record CreateTrackCommand(CreateTrackDto Dto) : IRequest<TrackDto>;
internal class CreateTrackCommandHandler(IApplicationDbContext _context) : IRequestHandler<CreateTrackCommand, TrackDto>
{
    public async Task<TrackDto> Handle(CreateTrackCommand request, CancellationToken ct)
    {
        var track = new Track 
        {
            Name = request.Dto.Name,
            Author = request.Dto.Author,
            Album = request.Dto.Album,
            Rating = request.Dto.Rating,
            HasFile = request.Dto.HasFile
        };

        await _context.Tracks.AddAsync(track);
        await _context.SaveChangesAsync();

        return new TrackDto
            (
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
