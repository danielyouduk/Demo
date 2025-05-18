using FleetManagementService.Application.Contracts.Persistence;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace FleetManagementService.Application.Validation;

public class DriverByIdMustExistAsync(
    IDriverRepository driverRepository,
    ILogger<DriverByIdMustExistAsync> logger) : AbstractValidator<Guid>
{
    public async Task<bool> ValidateAsync(Guid driverId, CancellationToken cancellationToken = default)
    {
        try
        {
            return await driverRepository.ExistsAsync(driverId, cancellationToken);
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception e)
        {
            // todo: Add Exception log message for DriverByIdMustExistAsync.ValidateAsync
            logger.LogError(e, string.Empty, driverId);
            throw;
        }
    }
}