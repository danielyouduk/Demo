using Azure;
using Azure.Data.Tables;
using Newtonsoft.Json;

namespace SearchService.Api.Models;

public class DriverEntity : ITableEntity
{
    public string PartitionKey { get; set; }
    public string RowKey { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
    
    public ETag ETag { get; set; }

    public string Category { get; set; }
    public string Title { get; set; }
    public string Url { get; set; }
}
