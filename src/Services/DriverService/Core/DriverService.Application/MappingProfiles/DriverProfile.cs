using AutoMapper;
using DriverService.Application.Features.Drivers.Commands.CreateDriver;
using DriverService.Application.Features.Drivers.Shared;
using DriverService.Domain.Entities;

namespace DriverService.Application.MappingProfiles;

public class DriverProfile : Profile
{
    public DriverProfile()
    {
        CreateMap<DriverDto, DriverEntity>().ReverseMap();
        CreateMap<CreateDriverCommand, DriverEntity>().ReverseMap();
    }
}