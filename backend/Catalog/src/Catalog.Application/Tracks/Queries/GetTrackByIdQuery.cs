using Catalog.Application.Interfaces;
using Catalog.Application.Tracks.Dtoes;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Application.Tracks.Queries;

public record GetTrackByIdQuery(Guid Id) : IRequest<TrackDto?>;
internal class GetTrackByIdQueryHandler(IApplicationDbContext _context) : IRequestHandler<GetTrackByIdQuery, TrackDto?>
{
    public async Task<TrackDto?> Handle(GetTrackByIdQuery request, CancellationToken ct)
    {
        return await _context.Tracks
            .AsNoTracking()
            .Where(t => t.Id == request.Id)
            .Select(t => new TrackDto
            (
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
