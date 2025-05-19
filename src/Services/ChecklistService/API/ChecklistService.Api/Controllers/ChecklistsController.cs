using ChecklistService.Application.Features.Checklist.Commands.CreateChecklist;
using ChecklistService.Application.Features.Checklist.Commands.DeleteChecklist;
using ChecklistService.Application.Features.Checklist.Commands.SubmitChecklist;
using ChecklistService.Application.Features.Checklist.Commands.UpdateChecklist;
using ChecklistService.Application.Features.Checklist.Queries.GetChecklists;
using ChecklistService.Application.Features.Checklist.Shared;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Services.Core.Enums;
using Services.Core.Models;
using Services.Core.Models.Service;

namespace ChecklistService.Api.Controllers;

// todo: Add Authorize attribute
// [Authorize]
[ApiController]
[Route("api/checklists")]
public class ChecklistsController(
    ISender mediator,
    ILogger<ChecklistsController> logger) : ControllerBase
{
    // GET api/checklists
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)] 
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetChecklists(
        [FromQuery] PagedRequestQuery query, CancellationToken cancellationToken = default)
    {
        try
        {
            var checklists = await mediator.Send(new GetChecklistsQuery(query), cancellationToken);
            return Ok(checklists);
        }
        catch (ValidationException e)
        {
            return BadRequest(new ServiceResponseCollection<IReadOnlyCollection<ChecklistDto>>
            {
                Status = ServiceStatus.Invalid,
                Message = e.Message,
                Data = null
            });
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception e)
        {
            // todo: Add Exception log message for ChecklistsController.GetChecklists
            logger.LogError(e, string.Empty, query);
            
            return BadRequest(new ServiceResponseCollection<IReadOnlyCollection<ChecklistDto>>
            { 
                Status = ServiceStatus.Failure, 
                Message = "Error processing request",
                Data = null
            });
        }
    }
    
    // GET api/checklists/:id
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetChecklistById(Guid id)
    {
        return Ok();
    }
    
    // POST api/checklists
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateChecklist(
        [FromBody] CreateChecklistCommand createChecklistCommand)
    {
        var result = await mediator.Send(createChecklistCommand);
        
        return CreatedAtAction(nameof(GetChecklistById), new { id = result.Data }, result);
    }
    
    // PUT api/checklists/:id
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateChecklist(
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
    public async Task<IActionResult> SubmitChecklist(
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
    public async Task<IActionResult> DeleteChecklist([FromRoute] Guid id, [FromQuery] string accountId)
    {
        var guid = Guid.Parse(accountId);
        
        await mediator.Send(new DeleteChecklistCommand(id, guid));
        
        return Ok();
    }
}