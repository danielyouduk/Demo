using Azure.Search.Documents;
using Azure.Search.Documents.Models;

namespace SearchService.Api.Services;

public class DriverSearchService(SearchClient searchClient)
{
    public async Task<SearchResults<SearchDocument>> SearchDriversAsync(string searchText)
    {
        return await searchClient.SearchAsync<SearchDocument>(searchText);
    }
}
