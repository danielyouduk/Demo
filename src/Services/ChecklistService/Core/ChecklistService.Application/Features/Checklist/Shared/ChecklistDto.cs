namespace ChecklistService.Application.Features.Checklist.Shared;

public class ChecklistDto
{
    public Guid Id { get; set; }
    
    public Guid AccountId { get; set; }
    
    public string? Title { get; set; }
    
    public bool IsSubmitted { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
}