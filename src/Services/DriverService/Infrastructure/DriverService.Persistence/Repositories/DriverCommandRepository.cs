using AutoMapper;
using DriverService.Application.Contracts.Persistence;
using DriverService.Application.Features.Drivers.Shared;
using DriverService.Domain.Entities;
using DriverService.Persistence.DatabaseContext;

namespace DriverService.Persistence.Repositories;

public class DriverCommandRepository(DriverDatabaseContext context, IMapper mapper) : IDriverCommandRepository
{
    public async Task<DriverDto> CreateDriver(DriverEntity driver)
    {
        await context.AddAsync(driver);
        await context.SaveChangesAsync();

        return mapper.Map<DriverDto>(driver);
    }

    public Task UpdateDriver(object driver)
    {
        throw new NotImplementedException();
    }
}