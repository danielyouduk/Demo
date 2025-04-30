using DriverService.Application.Contracts.Persistence;

namespace DriverService.Persistence.Repositories;

public class DriverRepository : IDriverRepository
{
    public Task<ICollection<object>> GetDrivers()
    {
        throw new NotImplementedException();
    }

    public Task<object> GetDriverById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<object> CreateDriver(object driver)
    {
        throw new NotImplementedException();
    }

    public Task UpdateDriver(object driver)
    {
        throw new NotImplementedException();
    }
}