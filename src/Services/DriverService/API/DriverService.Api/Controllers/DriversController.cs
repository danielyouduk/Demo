using DriverService.Application.Features.Drivers.Commands;
using Microsoft.AspNetCore.Mvc;

namespace DriverService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DriversController : ControllerBase
{
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return ["David", "John"];
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
}