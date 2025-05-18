using FleetManagementService.Application.Features.Account.Commands.CreateAccount;
using FleetManagementService.Application.Features.Account.Commands.UpdateAccount;
using FleetManagementService.Application.Features.Account.Queries.GetAccount;
using FleetManagementService.Application.Features.Account.Queries.GetAccounts;
using FleetManagementService.Application.Features.Account.Shared;
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
[Route("api/accounts")]
public class AccountsController(
    ISender mediator,
    ILogger<AccountsController> logger) : ControllerBase
{
    /// <summary>
    /// Retrieves a paginated collection of accounts based on the specified query parameters.
    /// </summary>
    /// <param name="query">An object containing the query parameters for pagination, sorting, and filtering.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A service response containing a collection of account DTOs.</returns>
    /// <response code="200">Returns the list of accounts</response>
    /// <response code="400">If the query parameters are invalid</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ServiceResponseCollection<List<AccountDto>>>> GetAccounts(
        [FromQuery] PagedRequestQuery query, CancellationToken cancellationToken = default)
    {
        try
        {
            var accounts = await mediator.Send(new GetAccountsQuery(query), cancellationToken);
            return Ok(accounts);
        }
        catch (ValidationException e)
        {
            return BadRequest(new ServiceResponseCollection<IReadOnlyCollection<AccountDto>>
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
            // todo: Add Exception log message for AccountsController.GetAccounts
            logger.LogError(e, string.Empty, query);
            
            return BadRequest(new ServiceResponseCollection<IReadOnlyCollection<AccountDto>>
            { 
                Status = ServiceStatus.Failure, 
                Message = "Error processing request",
                Data = null
            });
        }
    }

    /// <summary>
    /// Retrieves an account by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the account to retrieve.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A service response containing the account details if found, otherwise an error response.</returns>
    /// <response code="200">Returns the account data.</response>
    /// <response code="400">If the request is invalid or cannot be processed.</response>
    /// <response code="404">If the account with the specified identifier is not found.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ServiceResponse<AccountDto>>> GetAccountById(
        [FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await mediator.Send(new GetAccountQuery(id), cancellationToken);;
        
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
            return BadRequest(new ServiceResponse<AccountDto>
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
            // todo: Add Exception log message for AccountsController.GetAccountById
            logger.LogError(e, string.Empty, id);
            
            return BadRequest(new ServiceResponse<AccountDto> 
            { 
                Status = ServiceStatus.Failure, 
                Message = "Error processing request",
                Data = null
            });
        }
    }

    /// <summary>
    /// Creates a new account based on the provided account creation details.
    /// </summary>
    /// <param name="createAccountCommand">The command containing the required details for creating a new account.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A service response containing the details of the created account.</returns>
    /// <response code="201">Returns the details of the created account.</response>
    /// <response code="400">If the provided account details are invalid.</response>
    /// <response code="401">If the user is not authorized to create an account.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ServiceResponse<AccountDto>>> CreateAccount(
        [FromBody] CreateAccountCommand createAccountCommand, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await mediator.Send(createAccountCommand, cancellationToken);

            return CreatedAtAction(nameof(GetAccountById), new { id = result.Data.Id }, result);
        }
        catch (ValidationException e)
        {
            return BadRequest(new ServiceResponse<AccountDto>
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
            // todo: Add Exception log message for AccountsController.CreateAccount
            logger.LogError(e, string.Empty, createAccountCommand);
            
            return BadRequest(new ServiceResponse<AccountDto> 
            { 
                Status = ServiceStatus.Failure, 
                Message = "Error processing request",
                Data = null
            });
        }
    }

    /// <summary>
    /// Updates an existing account with the specified details.
    /// </summary>
    /// <param name="updateAccountCommand">The command object containing the details of the account to be updated.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A service response indicating the success or failure of the update operation.</returns>
    /// <response code="204">Indicates the account was successfully updated.</response>
    /// <response code="400">If the request data is invalid or the operation encounters a failure.</response>
    /// <response code="404">If the account to be updated is not found.</response>
    /// <response code="401">If the request is unauthorized.</response>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ServiceResponse<Unit>>> UpdateAccount(
        [FromBody] UpdateAccountCommand updateAccountCommand, CancellationToken cancellationToken = default)
    {
        try
        {
            await mediator.Send(updateAccountCommand, cancellationToken);

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
            // todo: Add Exception log message for AccountsController.UpdateAccount
            logger.LogError(e, string.Empty, updateAccountCommand);
            
            return BadRequest(new ServiceResponse<Unit> 
            { 
                Status = ServiceStatus.Failure, 
                Message = "Error processing request"
            });
        }
    }
    
    // DELETE api/accounts/:id
    // [Authorize(Roles = "Admin")]
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, [FromQuery] string accountId)
    {
        var guid = Guid.Parse(accountId);
        
        // todo: Add logic for deleting account
        // await mediator.Send(new DeleteAccountCommand(id, guid));
        
        return Ok();
    }
}