using DriverService.Application.Features.Drivers.Shared;
using DriverService.Domain.Entities;

namespace DriverService.Application.Contracts.Persistence;

public interface IDriverCommandRepository
{
    Task<DriverDto> CreateDriver(DriverEntity driver);
    
    Task UpdateDriver(object driver);
}