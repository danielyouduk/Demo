using Services.Core.Entities;

namespace AddressLookupService.Domain.Entities;

public class AddressEntity : BaseEntity
{
    public Guid AccountId { get; set; }
    
    public string? Street { get; set; }
    
    public string? Town { get; set; }
    
    public string? City { get; set; }
    
    public string? PostCode { get; set; }
}