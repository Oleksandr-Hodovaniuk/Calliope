using Catalog.Application.Common.Entities;
using Catalog.Application.Interfaces;
using Catalog.Application.Tracks.Dtoes;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Application.Tracks.Queries;

public record GetTracksQuery(int Page = 1) : IRequest<PagedResultDto<TrackDto>>;
internal class GetTracksQueryHandler(IApplicationDbContext _context) : IRequestHandler<GetTracksQuery, PagedResultDto<TrackDto>>
{
    public async Task<PagedResultDto<TrackDto>> Handle(GetTracksQuery request, CancellationToken ct)
    {
        const int pageSize = 50;

        var query = _context.Tracks.AsNoTracking();

        var totalCount = await query.CountAsync(ct);

        var tracks = await query
            .OrderByDescending(t => t.CreatedAt)
            .Skip((request.Page - 1) * pageSize)
            .Take(pageSize)
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
            .ToListAsync(ct);

        return new PagedResultDto<TrackDto>
        (
            tracks,
            request.Page,
            pageSize,
            totalCount
        );
    }
}
