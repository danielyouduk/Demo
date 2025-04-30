namespace DriverService.Application.Contracts.Persistence;

public interface IDriverRepository
{
    Task<ICollection<object>> GetDrivers();
    
    Task<object> GetDriverById(Guid id);
    
    Task<object> CreateDriver(object driver);
    
    Task UpdateDriver(object driver);
}