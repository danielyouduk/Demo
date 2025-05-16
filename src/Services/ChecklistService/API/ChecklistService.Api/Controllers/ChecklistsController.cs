using ChecklistService.Application.Features.Checklist.Commands.CreateChecklist;
using ChecklistService.Application.Features.Checklist.Commands.DeleteChecklist;
using ChecklistService.Application.Features.Checklist.Commands.SubmitChecklist;
using ChecklistService.Application.Features.Checklist.Commands.UpdateChecklist;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChecklistService.Api.Controllers;

// todo: Add Authorize attribute
// [Authorize]
[ApiController]
[Route("api/checklists")]
public class ChecklistsController(ISender mediator) : ControllerBase
{
    // GET api/checklists
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)] 
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get()
    {
        return Ok();
    }
    
    // GET api/checklists/:id
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        return Ok();
    }
    
    // POST api/checklists
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Post(
        [FromBody] CreateChecklistCommand createChecklistCommand)
    {
        var result = await mediator.Send(createChecklistCommand);
        
        return CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, result);
    }
    
    // PUT api/checklists/:id
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(
        [FromBody] UpdateChecklistCommand updateChecklistCommand)
    {
        await mediator.Send(updateChecklistCommand);
        
        return Ok();
    }

    // PUT api/checklists/:id/submit
    [HttpPut("{id:guid}/submit")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Submit(
        [FromRoute] Guid id,
        [FromBody] SubmitChecklistCommand submitChecklistCommand)
    {
        await mediator.Send(submitChecklistCommand);
        
        return Ok();   
    }

    // DELETE api/checklists/:id
    // [Authorize(Roles = "Admin")]
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, [FromQuery] string accountId)
    {
        var guid = Guid.Parse(accountId);
        
        await mediator.Send(new DeleteChecklistCommand(id, guid));
        
        return Ok();
    }
}