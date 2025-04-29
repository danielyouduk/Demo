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
    
    [HttpGet("{id}")]
    public string Get(Guid id)
    {
        return "David";
    }
}