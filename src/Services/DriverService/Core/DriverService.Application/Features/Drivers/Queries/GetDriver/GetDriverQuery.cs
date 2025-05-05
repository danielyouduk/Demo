using System.ComponentModel.DataAnnotations;
using DriverService.Application.Features.Drivers.Shared;
using MediatR;
using Services.Core.Models.Service;

namespace DriverService.Application.Features.Drivers.Queries.GetDriver;

public record GetDriverQuery : IRequest<ServiceResponse<DriverDto>>
{
    [Required]
    public Guid Id { get; set; }
}