using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BaseController(ISender _mediator) : ControllerBase()
{
    protected ISender Mediator { get; } = _mediator;
}