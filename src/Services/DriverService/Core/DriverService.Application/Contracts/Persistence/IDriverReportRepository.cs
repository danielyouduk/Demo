namespace DriverService.Application.Contracts.Persistence;

public interface IDriverReportRepository
{
    Task<ICollection<object>> GetDriverReports();
    
    Task<ICollection<object>> GetDriverReportsByDriverId(Guid driverId);
    
    Task<object> GetDriverReportById(Guid id);
    
    Task<object> CreateDriverReport(object driverReport);
    
    Task UpdateDriverReport(object driverReport);
}