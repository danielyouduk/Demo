using Microsoft.AspNetCore.Mvc;
using MongoDB.Entities;
using SearchService.Api.Models;
using SearchService.Api.RequestHelpers;

namespace SearchService.Api.Controllers;

[ApiController]
[Route("api/search")]
public class SearchController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Item>>> SearchItems(
        [FromQuery] SearchParams searchParams)
    {
        var query = DB.PagedSearch<Item, Item>();

        if (!string.IsNullOrWhiteSpace(searchParams.SearchTerm))
        {
            query.Match(Search.Full, searchParams.SearchTerm).SortByTextScore();
        }

        query = searchParams.OrderBy switch
        {
            "name" => query.Sort(x => x.Ascending(i => i.Name)),
            "type" => query.Sort(x => x.Ascending(i => i.Type)),
            _ => query.Sort(x => x.Descending(i => i.UpdatedAt))
        };

        if (!string.IsNullOrEmpty(searchParams.Name))
        {
            query.Match(x => x.Name.Contains(searchParams.Name));
        }

        if (!string.IsNullOrEmpty(searchParams.Type))
        {
            query.Match(x => x.Type.Contains(searchParams.Type));
        }
        
        query.PageNumber(searchParams.PageNumber);
        query.PageSize(searchParams.PageSize);

        var results = await query.ExecuteAsync();
        
        return Ok(new
        {
            results = results.Results,
            pageCount = results.PageCount,
            totalCount = results.TotalCount
        });
    }
}