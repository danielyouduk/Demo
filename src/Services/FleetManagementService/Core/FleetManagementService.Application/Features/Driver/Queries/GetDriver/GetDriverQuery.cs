using System.ComponentModel.DataAnnotations;
using FleetManagementService.Application.Features.Driver.Shared;
using MediatR;
using Services.Core.Models.Service;

namespace FleetManagementService.Application.Features.Driver.Queries.GetDriver;

public record GetDriverQuery : IRequest<ServiceResponse<DriverDto>>
{
    [Required]
    public Guid Id { get; set; }
}