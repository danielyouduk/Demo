using FleetManagementService.Application.Features.Driver.Commands.CreateDriver;
using FleetManagementService.Application.Features.Driver.Commands.UpdateDriver;
using FleetManagementService.Application.Features.Driver.Queries.GetDriver;
using FleetManagementService.Application.Features.Driver.Queries.GetDrivers;
using FleetManagementService.Application.Features.Driver.Shared;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Services.Core.Enums;
using Services.Core.Models;
using Services.Core.Models.Service;

namespace FleetManagementService.Api.Controllers;

// todo: Add Authorize attribute
[ApiController]
[Route("api/[controller]")]
public class DriversController(
    ISender mediator, 
    ILogger<DriversController> logger) : ControllerBase
{
    /// <summary>
    /// Retrieves a paginated list of drivers based on the specified query parameters.
    /// </summary>
    /// <param name="query">The query parameters for pagination, sorting, and filtering of drivers.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation if needed.</param>
    /// <returns>A service response containing a paginated collection of driver data.</returns>
    /// /// <response code="200">Returns the list of drivers</response>
    /// <response code="400">If the query parameters are invalid</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ServiceResponseCollection<IReadOnlyList<DriverDto>>>> GetDrivers(
        [FromQuery] PagedRequestQuery query, CancellationToken cancellationToken = default)
    {
        try
        {
            var drivers = await mediator.Send(
                new GetDriversQuery(query), cancellationToken);

            return Ok(drivers);
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception e)
        {
            // todo: Add Exception log message for DriversController.GetDrivers
            logger.LogError(e, string.Empty, query);
            
            return BadRequest(new ServiceResponseCollection<IReadOnlyCollection<DriverDto>>
            { 
                Status = ServiceStatus.Failure, 
                Message = "Error processing request",
                Data = null
            });
        }
    }

    /// <summary>
    /// Retrieves a driver by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the driver to retrieve.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation if needed.</param>
    /// <returns>A service response containing the driver data or null if not found.</returns>
    /// <response code="200">Returns the driver details if found.</response>
    /// <response code="400">If the request is invalid or processing fails.</response>
    /// <response code="404">If the driver is not found.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ServiceResponse<DriverDto?>>> GetDriverById(
        [FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await mediator.Send(new GetDriverQuery(id), cancellationToken);

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
            return BadRequest(new ServiceResponse<DriverDto>
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
            // todo: Add Exception log message for DriversController.GetDriverById
            logger.LogError(e, string.Empty, id);
            
            return BadRequest(new ServiceResponse<DriverDto> 
            { 
                Status = ServiceStatus.Failure, 
                Message = "Error processing request",
                Data = null
            });
        }
    }

    /// <summary>
    /// Creates a new driver with the specified details.
    /// </summary>
    /// <param name="command">The command containing details of the driver to be created, including account ID, first name, and last name.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation if needed.</param>
    /// <returns>A service response containing the ID of the newly created driver.</returns>
    /// <response code="201">Indicates that the driver has been successfully created.</response>
    /// <response code="400">If the provided driver details are invalid.</response>
    /// <response code="401">If the requester is unauthorized.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ServiceResponse<DriverDto>>> CreateDriver(
        [FromBody] CreateDriverCommand command, CancellationToken cancellationToken = default)
    {
        try
        {
            var newDriver = await mediator.Send(command, cancellationToken);
        
            return CreatedAtAction(nameof(GetDriverById), new { id = newDriver.Data }, newDriver);
        }
        catch (ValidationException e)
        {
            return BadRequest(new ServiceResponse<DriverDto>
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
            // todo: Add Exception log message for DriversController.CreateDriver
            logger.LogError(e, string.Empty, command);
            
            return BadRequest(new ServiceResponse<DriverDto> 
            { 
                Status = ServiceStatus.Failure, 
                Message = "Error processing request",
                Data = null
            });
        }
    }

    /// <summary>
    /// Updates the details of an existing driver specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the driver to be updated.</param>
    /// <param name="command">The command containing the updated driver details.</param>
    /// <param name="cancellationToken">A token to cancel the operation if required.</param>
    /// <returns>A service response representing the result of the update operation.</returns>
    /// <response code="204">The driver details were successfully updated.</response>
    /// <response code="400">The request is invalid or could not be processed.</response>
    /// <response code="500">An error occurred while processing the update request.</response>
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ServiceResponse<Unit>>> UpdateDriver
        (Guid id, [FromBody] UpdateDriverCommand command, CancellationToken cancellationToken = default)
    {
        try
        {
            await mediator.Send(command, cancellationToken);

            return NoContent();
        }
        catch (ValidationException e)
        {
            return BadRequest(new ServiceResponse<Unit>
            {
                Status = ServiceStatus.Invalid,
                Message = e.Message
            });
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception e)
        {
            // todo: Add Exception log message for DriversController.UpdateDriver
            logger.LogError(e, string.Empty, command);
            
            return BadRequest(new ServiceResponse<Unit> 
            { 
                Status = ServiceStatus.Failure, 
                Message = "Error processing request"
            });
        }
    }
}