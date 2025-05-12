using ChecklistService.Application.Features.Checklist.Commands.CreateChecklist;
using ChecklistService.Application.Features.Checklist.Commands.DeleteChecklist;
using ChecklistService.Application.Features.Checklist.Commands.SubmitChecklist;
using ChecklistService.Application.Features.Checklist.Commands.UpdateChecklist;
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
    
    [HttpGet("{id:guid}")]
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
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Put(
        [FromBody] UpdateChecklistCommand updateChecklistCommand)
    {
        await mediator.Send(updateChecklistCommand);
        
        return Ok();
    }

    [HttpPut("{id:guid}/submit")]
    public async Task<IActionResult> Submit(
        [FromRoute] Guid id,
        [FromBody] SubmitChecklistCommand submitChecklistCommand)
    {
        await mediator.Send(submitChecklistCommand);
        
        return Ok();   
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id, [FromQuery] string accountId)
    {
        var guid = Guid.Parse(accountId);
        
        await mediator.Send(new DeleteChecklistCommand(id, guid));
        
        return Ok();
    }
}