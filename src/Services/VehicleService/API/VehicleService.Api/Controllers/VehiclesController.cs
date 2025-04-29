using Microsoft.AspNetCore.Mvc;

namespace VehicleService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VehiclesController : ControllerBase
{
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return ["Scania", "Volvo"];
    }
    
    [HttpGet("{id:guid}")]
    public string Get(Guid id)
    {
        return "Scania";
    }
}