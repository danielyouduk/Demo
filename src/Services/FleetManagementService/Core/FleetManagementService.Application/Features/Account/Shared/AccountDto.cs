namespace FleetManagementService.Application.Features.Account.Shared;

public class AccountDto
{
    public Guid Id { get; set; }
    
    public string? CompanyName { get; set; }
    
    public string? CompanyVatNumber { get; set; }
    
    public Guid? BillingAddressId { get; set; }
    
    public int NoOfActiveDrivers { get; set; }
    
    public int NoOfActiveVehicles { get; set; }
    
    public int NoOfActiveChecklists { get; set; }
    
    public int NoOfChecklistsSubmitted { get; set; }
    
    public bool IsActive { get; init; }
    
    public DateTime? LastChecklistSubmittedAt { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
}