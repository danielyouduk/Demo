using Microsoft.AspNetCore.Mvc;

namespace AccountService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountsController : ControllerBase
{
    [HttpGet]
    public string Get()
    {
        return "DYOUD";
    }
}