using FleetManagementService.Application.Features.Driver.Commands.CreateDriver;
using FleetManagementService.Application.Features.Driver.Commands.UpdateDriver;
using FleetManagementService.Application.Features.Driver.Queries.GetDriver;
using FleetManagementService.Application.Features.Driver.Queries.GetDrivers;
using FleetManagementService.Application.Features.Driver.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Services.Core.Models;
using Services.Core.Models.Service;

namespace FleetManagementService.Api.Controllers;

// todo: Add Authorize attribute
[ApiController]
[Route("api/[controller]")]
public class DriversController(ISender mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)] 
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ServiceResponseCollection<IReadOnlyList<DriverDto>>>> GetDrivers(
        [FromQuery] PagedRequestQuery pagedRequestQuery)
    {
        var drivers = await mediator.Send(
            new GetDriversQuery(pagedRequestQuery));

        return Ok(drivers);
    }
    
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ServiceResponse<DriverDto?>>> Get([FromRoute] Guid id)
    {
        var response = await mediator.Send(new GetDriverQuery { Id = id });

        if (response.Data is null)
        {
            return NotFound();
        }

        return Ok(response);
    }
    
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CreateDriverCommand command)
    {
        var newDriver = await mediator.Send(command);
        
        return CreatedAtAction(nameof(Get), new { id = newDriver.Data }, newDriver);
    }

    [HttpPut("{id:guid}")]
    public ActionResult Put(Guid id, [FromBody] UpdateDriverCommand command)
    {
        return Ok();
    }
}