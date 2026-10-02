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
            .AsNoTracking()
            .Where(t => t.Id == request.Id)
            .Select(t => new TrackDto(
                t.Id,
                t.Name,
                t.Author,
                t.Album,
                t.Rating,
                t.HasFile,
                t.CreatedAt
                ))
            .FirstOrDefaultAsync(ct); 
    }
}