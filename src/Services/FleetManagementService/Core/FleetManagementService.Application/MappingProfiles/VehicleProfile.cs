using AutoMapper;
using FleetManagementService.Application.Features.Vehicle.Shared;
using FleetManagementService.Domain.Entities;

namespace FleetManagementService.Application.MappingProfiles;

public class VehicleProfile : Profile
{
    public VehicleProfile()
    {
        #region Command Mappings
        
        
        #endregion
        
        #region Query Mappings

        CreateMap<Vehicle, VehicleDto>();

        #endregion
    }
}