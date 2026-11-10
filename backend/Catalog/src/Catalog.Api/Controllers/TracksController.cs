using Catalog.Application.Tracks.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers;

public class TracksController(ISender Mediator) : BaseController(Mediator)
{
    [HttpGet]
    public async Task<IActionResult> GetTracks([FromQuery] int page = 1, CancellationToken ct = default)
    {
        page = Math.Max(page, 1);

        var result = await Mediator.Send(new GetTracksQuery(page), ct);

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetTrackById([FromRoute] Guid id, CancellationToken ct = default)
    {
        var result = await Mediator.Send(new GetTrackByIdQuery(id), ct);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }
}
