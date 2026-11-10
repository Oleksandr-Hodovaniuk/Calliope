using Catalog.Application.Tracks.Commands;
using Catalog.Application.Tracks.Dtoes;
using Catalog.Application.Tracks.Queries;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
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

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteTrack([FromRoute] Guid id, CancellationToken ct = default)
    {
        await Mediator.Send(new DeleteTrackCommand(id), ct);

        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> CreateTrack([FromBody] CreateTrackDto dto, CancellationToken ct = default)
    {
        return Ok(await Mediator.Send(new CreateTrackCommand(dto), ct));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateTrack([FromRoute] Guid id, [FromBody] UpdateTrackDto dto, CancellationToken ct = default)
    {
        return Ok(await Mediator.Send(new UpdateTrackCommand(id, dto), ct));
    }
}
