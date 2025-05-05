namespace Services.Core.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; set; }
    
    public bool IsActive { get; init; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
}