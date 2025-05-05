using SearchService.Api.Models;

namespace SearchService.Api.Repositories;

public interface IDriverCosmosRepository
{
    Task AddDriverAsync(Driver driver);

    Task<Driver> GetDriverByIdAsync(string id);
}