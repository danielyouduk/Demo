using Microsoft.AspNetCore.Mvc;
using SearchService.Api.Services;

namespace SearchService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SearchController(DriverSearchService searchService) : ControllerBase
{
    [HttpGet("search")]
    public async Task<IActionResult> SearchDrivers([FromQuery] string query)
    {
        var searchResults = await searchService.SearchDriversAsync(query);

        var results = searchResults.GetResults().Select(r => r.Document);

        return Ok(results);
    }

}