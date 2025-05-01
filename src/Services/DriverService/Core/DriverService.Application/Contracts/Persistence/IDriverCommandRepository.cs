namespace DriverService.Application.Contracts.Persistence;

public interface IDriverCommandRepository
{
    Task<object> CreateDriver(object driver);
    
    Task UpdateDriver(object driver);
}