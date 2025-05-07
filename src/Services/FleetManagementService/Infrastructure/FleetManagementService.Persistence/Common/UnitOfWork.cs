using FleetManagementService.Application.Contracts.Persistence.Common;
using FleetManagementService.Persistence.DatabaseContext;

namespace FleetManagementService.Persistence.Common;

public class UnitOfWork(FleetManagementDatabaseContext context) : IUnitOfWork
{
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await context.SaveChangesAsync(cancellationToken);
    }
}
