using FleetManagementService.Application.Features.Account.Queries.GetAccount;
using FleetManagementService.Application.Features.Account.Queries.GetAccounts;
using FleetManagementService.Application.Features.Account.Shared;
using MassTransit;
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
    public async Task<ActionResult<ServiceResponse<AccountDto>>> Get(
        [FromRoute] Guid id)
    {
        var account = await mediator.Send(new GetAccountQuery(id));
        
        return Ok(account);
    }
}