using FleetManagementService.Application.Features.Account.Commands.CreateAccount;
using FleetManagementService.Application.Features.Account.Commands.UpdateAccount;
using FleetManagementService.Application.Features.Account.Queries.GetAccount;
using FleetManagementService.Application.Features.Account.Queries.GetAccounts;
using FleetManagementService.Application.Features.Account.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Services.Core.Models;
using Services.Core.Models.Service;

namespace FleetManagementService.Api.Controllers;

// todo: Add Authorize attribute
[ApiController]
[Route("api/accounts")]
public class AccountsController(ISender mediator) : ControllerBase
{
    // GET api/accounts
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)] 
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ServiceResponseCollection<List<AccountDto>>>> Get(
        [FromQuery] PagedRequestQuery pagedRequestQuery)
    {
        var accounts = await mediator.Send(new GetAccountsQuery(pagedRequestQuery));
        
        return Ok(accounts);
    }
    
    // GET api/accounts/:id
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ServiceResponse<AccountDto>>> GetAccountById(
        [FromRoute] Guid id)
    {
        var account = await mediator.Send(new GetAccountQuery(id));
        
        return Ok(account);
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
}