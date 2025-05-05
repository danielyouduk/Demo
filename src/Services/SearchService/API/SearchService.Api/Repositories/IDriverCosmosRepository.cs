using Azure.Data.Tables;
using SearchService.Api.Models;

namespace SearchService.Api.Repositories;

public interface IDriverCosmosRepository
{
    Task AddDriverAsync(DriverEntity driver);

    Task<ITableEntity> GetDriverByIdAsync(string id);
}