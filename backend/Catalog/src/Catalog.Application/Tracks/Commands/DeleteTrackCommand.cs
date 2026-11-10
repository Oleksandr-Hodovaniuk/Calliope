using Catalog.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Application.Tracks.Commands;

public record DeleteTrackCommand(Guid Id) : IRequest;
internal class DeleteTrackCommandHandler(IApplicationDbContext _context) : IRequestHandler<DeleteTrackCommand>
{
    public async Task Handle(DeleteTrackCommand request, CancellationToken ct)
    {
        var track = await _context.Tracks
            .FirstOrDefaultAsync(t => t.Id == request.Id, ct);

        if (track == null)
        {
            throw new KeyNotFoundException($"Track with id {request.Id} was not found.");
        }

        _context.Tracks.Remove(track);
        await _context.SaveChangesAsync();
    }
}
