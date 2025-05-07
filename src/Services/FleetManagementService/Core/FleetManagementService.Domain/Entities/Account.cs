using System.ComponentModel.DataAnnotations;
using Services.Core.Entities;

namespace FleetManagementService.Domain.Entities;

public class Account : BaseEntity
{
    [MaxLength(50)]
    public string? CompanyName { get; set; }
    
    public string? CompanyVatNumber { get; set; }
    
    public Guid? BillingAddressId { get; set; }
    
    public int NoOfActiveDrivers { get; set; }
    
    public int NoOfActiveVehicles { get; set; }
    
    public int NoOfActiveChecklists { get; set; }
    
    public int NoOfReportsSubmitted { get; set; }
    
    public ICollection<Driver>? Drivers { get; set; }

    public ICollection<Vehicle>? Vehicles { get; set; }
}