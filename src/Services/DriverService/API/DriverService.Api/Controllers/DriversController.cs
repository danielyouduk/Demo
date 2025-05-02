using DriverService.Application.Features.Drivers.Commands.CreateDriver;
using DriverService.Application.Features.Drivers.Commands.UpdateDriver;
using DriverService.Application.Features.Drivers.Queries.GetDrivers;
using DriverService.Application.Features.Drivers.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Services.Core.Models;
using Services.Core.Models.Service;

namespace DriverService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DriversController(ISender mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ServiceResponseCollection<IReadOnlyList<DriverDto>>>> GetDrivers(
        [FromQuery] PagedRequestQuery pagedRequestQuery)
    {
        var drivers = await mediator.Send(
            new GetDriversQuery(pagedRequestQuery));

        return Ok(drivers);
    }
    
    [HttpGet("{id:guid}")]
    public string Get(Guid id)
    {
        return "David";
    }
    
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CreateDriverCommand command)
    {
        return CreatedAtAction(nameof(Get), await mediator.Send(command));
    }

    [HttpPut("{id:guid}")]
    public ActionResult Put(Guid id, [FromBody] UpdateDriverCommand command)
    {
        return Ok();
    }
}