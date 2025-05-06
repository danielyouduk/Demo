using AutoMapper;
using FleetManagementService.Application.Features.Account.Shared;
using FleetManagementService.Domain.Entities;

namespace FleetManagementService.Application.MappingProfiles;

public class AccountProfile : Profile
{
    public AccountProfile()
    {
        #region Command Mappings
        
        
        #endregion
        
        #region Query Mappings

        CreateMap<Account, AccountDto>();

        #endregion
    }
}