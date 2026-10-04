using Catalog.Application.Tracks.Commands;
using Catalog.Application.Tracks.DTOs;
using Catalog.Application.Tracks.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers;

public class TracksController(ISender mediator) : BaseController(mediator)
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetTrack(Guid id, CancellationToken ct)
    {
        var result = await Mediator.Send(new GetTrackQuery(id), ct);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetTracks([FromQuery] int page = 1, CancellationToken ct = default)
    {
        page = Math.Max(page, 1);

        var result = await Mediator.Send(new GetTracksQuery(page), ct);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTrack([FromBody] CreateTrackDto dto, CancellationToken ct = default)
    {
        return Ok(await Mediator.Send(new CreateTrackCommand(dto), ct));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteTrack(Guid id, CancellationToken ct = default)
    {
        await Mediator.Send(new DeleteTrackCommand(id), ct);

        return NoContent();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateTrack(Guid id, [FromBody] UpdateTrackDto dto, CancellationToken ct = default)
    {
        var result = await Mediator.Send(new UpdateTrackCommand(id, dto), ct);

        return Ok(result);
    }
}
