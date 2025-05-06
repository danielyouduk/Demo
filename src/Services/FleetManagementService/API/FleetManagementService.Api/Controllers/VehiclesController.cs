using FleetManagementService.Application.Features.Vehicle.Queries.GetVehicle;
using FleetManagementService.Application.Features.Vehicle.Queries.GetVehicles;
using FleetManagementService.Application.Features.Vehicle.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Services.Core.Models;
using Services.Core.Models.Service;

namespace FleetManagementService.Api.Controllers;

[ApiController]
[Route("api/vehicles")]
public class VehiclesController(ISender mediator) : ControllerBase
{
    // GET api/vehicles
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)] 
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ServiceResponseCollection<List<VehicleDto>>>> Get(
        [FromQuery] PagedRequestQuery pagedRequestQuery)
    {
        var vehicles = await mediator.Send(
            new GetVehiclesQuery(pagedRequestQuery));
        
        return Ok(vehicles);
    }
    
    // GET api/vehicles/:id
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ServiceResponse<VehicleDto>>> Get(
        [FromRoute] Guid id)
    {
        var vehicle = await mediator.Send(new GetVehicleQuery(id));
        
        return Ok(vehicle);
    }
    
    [HttpPost]
    public ActionResult Post([FromBody] string value)
    {
        return CreatedAtAction(nameof(Get), new { id = Guid.NewGuid() }, value);
    }
}