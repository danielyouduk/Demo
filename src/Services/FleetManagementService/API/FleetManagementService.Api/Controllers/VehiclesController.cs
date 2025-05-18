using FleetManagementService.Application.Features.Account.Shared;
using FleetManagementService.Application.Features.Vehicle.Commands.CreateVehicle;
using FleetManagementService.Application.Features.Vehicle.Queries.GetVehicle;
using FleetManagementService.Application.Features.Vehicle.Queries.GetVehicles;
using FleetManagementService.Application.Features.Vehicle.Shared;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Services.Core.Enums;
using Services.Core.Models;
using Services.Core.Models.Service;

namespace FleetManagementService.Api.Controllers;

// todo: Add Authorize attribute
// [Authorize]
[ApiController]
[Route("api/vehicles")]
public class VehiclesController(
    ISender mediator,
    ILogger<VehiclesController> logger) : ControllerBase
{
    /// <summary>
    /// Retrieves a paged list of vehicles based on the provided query parameters.
    /// </summary>
    /// <param name="query">The pagination and filtering parameters for the vehicle retrieval.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A response containing a collection of vehicles along with pagination metadata.
    /// Returns a 200 OK status for successful operations,
    /// or a 400 Bad Request status if the request contains validation errors or fails due to other reasons.
    /// </returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ServiceResponseCollection<List<VehicleDto>>>> GetVehicles(
        [FromQuery] PagedRequestQuery query, CancellationToken cancellationToken = default)
    {
        try
        {
            var vehicles = await mediator.Send(new GetVehiclesQuery(query), cancellationToken);
            return Ok(vehicles);
        }
        catch (ValidationException e)
        {
            return BadRequest(new ServiceResponseCollection<IReadOnlyCollection<VehicleDto>>
            {
                Status = ServiceStatus.Invalid,
                Message = e.Message,
                Data = null
            });
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception e)
        {
            // todo: Add Exception log message for VehiclesController.GetVehicles
            logger.LogError(e, string.Empty, query);
            
            return BadRequest(new ServiceResponseCollection<IReadOnlyCollection<VehicleDto>>
            { 
                Status = ServiceStatus.Failure, 
                Message = "Error processing request",
                Data = null
            });
        }
    }

    /// <summary>
    /// Retrieves the details of a specific vehicle by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the vehicle to retrieve.</param>
    /// <param name="cancellationToken">A cancellation token to monitor for request cancellation.</param>
    /// <returns>
    /// A response containing the details of the requested vehicle if found.
    /// Returns a 200 OK status for a successful retrieval,
    /// a 404 Not Found status if the vehicle does not exist,
    /// or a 400 Bad Request status if the request processing encounters validation or other errors.
    /// </returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ServiceResponse<VehicleDto>>> GetVehicleById(
        [FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await mediator.Send(new GetVehicleQuery(id), cancellationToken);;
        
            if (response.Status != ServiceStatus.Success)
            {
                return response.Message.Contains("not found", StringComparison.OrdinalIgnoreCase) 
                    ? NotFound(response) 
                    : BadRequest(response);
            }

            return Ok(response);
        }
        catch (ValidationException e)
        {
            return BadRequest(new ServiceResponse<VehicleDto>
            {
                Status = ServiceStatus.Invalid,
                Message = e.Message,
                Data = null
            });
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception e)
        {
            // todo: Add Exception log message for VehiclesController.GetVehicleById
            logger.LogError(e, string.Empty, id);
            
            return BadRequest(new ServiceResponse<VehicleDto> 
            { 
                Status = ServiceStatus.Failure, 
                Message = "Error processing request",
                Data = null
            });
        }
    }

    /// <summary>
    /// Handles the creation of a new vehicle using the provided command details.
    /// </summary>
    /// <param name="createVehicleCommand">The command containing details required to create a vehicle, such as account ID and registration number.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests during the operation.</param>
    /// <returns>
    /// A response containing the details of the created vehicle including its ID.
    /// Returns a 201 Created status if the vehicle is successfully created,
    /// a 400 Bad Request status if the request contains validation errors, or
    /// a 500 Internal Server Error status if another error occurs during processing.
    /// </returns>
    [HttpPost]
    public async Task<ActionResult<ServiceResponse<VehicleDto>>> CreateVehicle(
        [FromBody] CreateVehicleCommand createVehicleCommand, CancellationToken cancellationToken)
    {
        try
        {
            var result = await mediator.Send(createVehicleCommand, cancellationToken);

            return CreatedAtAction(nameof(GetVehicleById), new { id = result.Data }, result);
        }
        catch (ValidationException e)
        {
            return BadRequest(new ServiceResponse<VehicleDto>
            {
                Status = ServiceStatus.Invalid,
                Message = e.Message,
                Data = null
            });
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception e)
        {
            // todo: Add Exception log message for VehiclesController.CreateVehicle
            logger.LogError(e, string.Empty, createVehicleCommand);
            
            return BadRequest(new ServiceResponse<VehicleDto>
            { 
                Status = ServiceStatus.Failure, 
                Message = "Error processing request",
                Data = null
            });
        }
    }
}