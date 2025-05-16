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
public class AccountsController(ISender mediator) : ControllerBase
{
    /// <summary>
    /// Retrieves a paginated collection of accounts based on the specified query parameters.
    /// </summary>
    /// <param name="query">An object containing the query parameters for pagination, sorting, and filtering.</param>
    /// <returns>A service response containing a collection of account DTOs.</returns>
    /// <response code="200">Returns the list of accounts</response>
    /// <response code="400">If the query parameters are invalid</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ServiceResponseCollection<List<AccountDto>>>> GetAccounts(
        [FromQuery] PagedRequestQuery query)
    {
        try
        {
            var accounts = await mediator.Send(new GetAccountsQuery(query));
            return Ok(accounts);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Message);
        }

    }

    /// <summary>
    /// Retrieves an account by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the account to retrieve.</param>
    /// <returns>A service response containing the account details if found, otherwise an error response.</returns>
    /// <response code="200">Returns the account data.</response>
    /// <response code="400">If the request is invalid or cannot be processed.</response>
    /// <response code="404">If the account with the specified identifier is not found.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ServiceResponse<AccountDto>>> GetAccountById(
        [FromRoute] Guid id)
    {
        try
        {
            var response = await mediator.Send(new GetAccountQuery(id));
        
            if (response.Status != ServiceStatus.Success)
            {
                return response.Message.Contains("not found", StringComparison.OrdinalIgnoreCase) 
                    ? NotFound(response) 
                    : BadRequest(response);
            }

            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(new ServiceResponse<AccountDto> 
            { 
                Status = ServiceStatus.Failure, 
                Message = "Error processing request",
                Data = null
            });
        }
    }
    
    // POST api/accounts
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ServiceResponse<AccountDto>>> Post(
        [FromBody] CreateAccountCommand createAccountCommand)
    {
        var result = await mediator.Send(createAccountCommand);

        return CreatedAtAction(nameof(GetAccountById), new { id = result.Data.Id }, result);
    }
    
    // PUT api/accounts/:id
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ServiceResponse<Unit>>> Put(
        [FromBody] UpdateAccountCommand updateAccountCommand)
    {
        await mediator.Send(updateAccountCommand);

        return NoContent();
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