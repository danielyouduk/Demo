using DriverService.Application.Features.Drivers.Commands;
using DriverService.Application.Features.Drivers.Queries.GetDrivers;
using DriverService.Application.Features.Drivers.Shared;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Services.Core.Helpers;
using Services.Core.Models;
using Services.Core.Models.Service;

namespace DriverService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DriversController(ISender mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ServiceResponseCollection<IReadOnlyList<DriverDto>>>> GetDrivers(
        [FromQuery] PagedRequestQuery pgedRequestQuery)
    {
        try
        {
            var pagedRequest = pgedRequestQuery.ToPagedRequest();
            
            var drivers = await mediator.Send(
                new GetDriversQuery(pagedRequest));

            return Ok(drivers);
        }
        catch (RequestFaultException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpGet("{id:guid}")]
    public string Get(Guid id)
    {
        return "David";
    }
    
    [HttpPost]
    public ActionResult Post([FromBody] CreateDriverCommand command)
    {
        return CreatedAtAction(nameof(Get), new { id = Guid.NewGuid() }, command);
    }

    [HttpPut("{id:guid}")]
    public ActionResult Put(Guid id, [FromBody] UpdateDriverCommand command)
    {
        return Ok();
    }
}