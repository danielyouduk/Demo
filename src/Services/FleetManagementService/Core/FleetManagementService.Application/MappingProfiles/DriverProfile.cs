using AutoMapper;
using FleetManagementService.Application.Features.Driver.Commands.CreateDriver;
using FleetManagementService.Application.Features.Driver.Shared;
using FleetManagementService.Domain.Entities;
using Services.Core.Events.DriverEvents;

namespace FleetManagementService.Application.MappingProfiles;

public class DriverProfile : Profile
{
    public DriverProfile()
    {
        #region Command Mappings
        
        CreateMap<CreateDriverCommand, Driver>();
        
        CreateMap<DriverDto, DriverCreated>()
            .ForMember(dest => dest.DriverId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.ResourceUrl, opt => opt.MapFrom(src => $"/api/drivers/{src.Id}"));
        
        #endregion
        
        #region Query Mappings

        CreateMap<Driver, DriverDto>();

        #endregion
    }
}