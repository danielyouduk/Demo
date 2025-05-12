using ChecklistService.Application.Features.Checklist.Commands.CreateChecklist;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChecklistService.Api.Controllers;

[ApiController]
[Route("api/checklists")]
public class ChecklistsController(ISender mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok();
    }
    
    [HttpGet("id:guid")]
    public async Task<IActionResult> GetById(Guid id)
    {
        return Ok();
    }
    
    [HttpPost]
    public async Task<IActionResult> Post(
        [FromBody] CreateChecklistCommand createChecklistCommand)
    {
        var result = await mediator.Send(createChecklistCommand);
        
        return CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, result);
    }
    
    [HttpPut]
    public async Task<IActionResult> Put()
    {
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete()
    {
        return Ok();
    }
}