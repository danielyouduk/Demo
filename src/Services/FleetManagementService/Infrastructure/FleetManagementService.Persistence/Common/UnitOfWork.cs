using FleetManagementService.Application.Contracts.Persistence.Common;
using FleetManagementService.Persistence.DatabaseContext;
using FleetManagementService.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FleetManagementService.Persistence.Common;

public class UnitOfWork(
    FleetManagementDatabaseContext context,
    ILogger<AccountRepository> logger) : IUnitOfWork
{
    private const string ConcurrencyErrorMessage = "A concurrency conflict occurred while saving changes";
    private const string DbUpdateErrorMessage = "An error occurred while saving changes to the database";
    private const string UnexpectedErrorMessage = "An unexpected error occurred while saving changes to the database";
    
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await context.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            logger.LogError(ex, ConcurrencyErrorMessage);
            throw new Exception(ConcurrencyErrorMessage, ex);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, DbUpdateErrorMessage);
            throw new Exception(DbUpdateErrorMessage, ex);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, UnexpectedErrorMessage);
            throw new Exception(UnexpectedErrorMessage, ex);
        }
    }
}
