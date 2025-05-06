using Services.Core.Entities;

namespace AccountService.Domain.Entities;

public class Account : BaseEntity
{
    public string? CompanyName { get; set; }
    
    public string? CompanyVatNumber { get; set; }
    
    public Guid BillingAddressId { get; set; }
    
    public int NoOfActiveDrivers { get; set; }
    
    public int NoOfActiveVehicles { get; set; }
    
    public int NoOfActiveChecklists { get; set; }
    
    public int NoOfReportsSubmitted { get; set; }
}