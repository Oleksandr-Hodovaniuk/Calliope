using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class BaseController(ISender _mediator) : ControllerBase()
{
    protected ISender Mediator { get; } = _mediator;
}