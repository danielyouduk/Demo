namespace FleetManagementService.Application.Contracts.Persistence.Common;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}