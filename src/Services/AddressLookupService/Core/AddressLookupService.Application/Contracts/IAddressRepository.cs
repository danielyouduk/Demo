using AddressLookupService.Application.Features.Address.Shared;
using Services.Core.Models;

namespace AddressLookupService.Application.Contracts;

public interface IAddressRepository
{
    Task<BasePagedResult<AddressDto>> GetAddressesAsync(PagedRequestQuery pagedRequestQuery);
    
    Task<AddressDto> GetAddressByIdAsync(Guid id);
}