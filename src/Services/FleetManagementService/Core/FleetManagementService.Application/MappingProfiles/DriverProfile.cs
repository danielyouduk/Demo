using AutoMapper;
using FleetManagementService.Application.Features.Driver.Commands.CreateDriver;
using FleetManagementService.Application.Features.Driver.Shared;
using FleetManagementService.Domain.Entities;

namespace FleetManagementService.Application.MappingProfiles;

public class DriverProfile : Profile
{
    public DriverProfile()
    {
        #region Command Mappings
        
        CreateMap<CreateDriverCommand, Driver>().ReverseMap();
        
        #endregion
        
        #region Query Mappings

        CreateMap<DriverDto, Driver>().ReverseMap();

        #endregion
    }
}