using Catalog.Application.Interfaces;
using Catalog.Application.Tracks.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Application.Tracks.Queries;

public record GetTrackQuery(Guid Id) : IRequest<TrackDto?>;
internal class GetTrackQueryHandler(IApplicationDbContext _context) : IRequestHandler<GetTrackQuery, TrackDto?>
{
    public async Task<TrackDto?> Handle(GetTrackQuery request, CancellationToken ct)
    {
        return await _context.Tracks
            .Where(x => x.Id == request.Id)
            .Select(x => new TrackDto(
                x.Id,
                x.Name,
                x.Author,
                x.Album,
                x.Rating,
                x.HasFile,
                x.CreatedAt
                ))
            .FirstOrDefaultAsync(ct); 
    }
}