using DriverService.Application.Contracts.Persistence;

namespace DriverService.Persistence.Repositories;

public class DriverReportRepository : IDriverReportRepository
{
    public Task<ICollection<object>> GetDriverReports()
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<object>> GetDriverReportsByDriverId(Guid driverId)
    {
        throw new NotImplementedException();
    }

    public Task<object> GetDriverReportById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<object> CreateDriverReport(object driverReport)
    {
        throw new NotImplementedException();
    }

    public Task UpdateDriverReport(object driverReport)
    {
        throw new NotImplementedException();
    }
}