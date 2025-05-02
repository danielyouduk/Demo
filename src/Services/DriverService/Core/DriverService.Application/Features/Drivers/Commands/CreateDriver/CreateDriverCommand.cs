using System.ComponentModel.DataAnnotations;
using MediatR;
using Services.Core.Models.Service;

namespace DriverService.Application.Features.Drivers.Commands.CreateDriver;

public class CreateDriverCommand : IRequest<ServiceResponse<Guid>>
{
    [Required]
    public required Guid AccountId { get; set; }
    
    [Required]
    [MinLength(3)]
    [MaxLength(50)]
    public required string FirstName { get; set; }
    
    [Required]
    [MinLength(3)]
    [MaxLength(50)]
    public required string LastName { get; set; }
}