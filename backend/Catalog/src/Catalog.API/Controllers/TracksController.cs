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
}
