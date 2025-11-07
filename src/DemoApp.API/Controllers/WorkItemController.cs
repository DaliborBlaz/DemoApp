using DemoApp.Application.WorkItems.Commands;
using DemoApp.Application.WorkItems.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DemoApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkItemController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public WorkItemController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] CreateWorkItemCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = result }, result);
    }
    
    
    // Stub method so CreatedAtAction doesn't error – implement later
    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        return Ok(); // placeholder
    }
    
    
    [HttpGet]
    [Authorize] // keep it secured
    public async Task<IActionResult> Get([FromQuery] GetWorkItemsQuery query, CancellationToken ct)
    {
        var result = await _mediator.Send(query, ct);
        return Ok(result);
    }
}