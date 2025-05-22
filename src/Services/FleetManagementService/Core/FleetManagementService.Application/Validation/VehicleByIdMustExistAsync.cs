using FleetManagementService.Application.Contracts.Persistence;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace FleetManagementService.Application.Validation;

public class VehicleByIdMustExistAsync(
    IVehicleRepository vehicleRepository,
    ILogger<VehicleByIdMustExistAsync> logger) : AbstractValidator<Guid>
{
    public async Task<bool> ValidateAsync(Guid vehicleId, CancellationToken cancellationToken = default)
    {
        try
        {
            return await vehicleRepository.ExistsAsync(vehicleId, cancellationToken);
        }
        catch (OperationCanceledException)
        {
            logger.LogInformation("Operation '{Operation}' was cancelled", nameof(ValidateAsync));
            throw;
        }
        catch (Exception e)
        {
            // todo: Add Exception log message for VehicleByIdMustExistAsync.ValidateAsync
            logger.LogError(e, string.Empty, vehicleId);
            throw;
        }       
    }
}