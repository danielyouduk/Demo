namespace Services.Core.Events.ChecklistsEvents;

public class ChecklistCreated
{
    public Guid ChecklistId { get; init; }
    
    public Guid AccountId { get; init; }
    
    public DateTime CreatedAt { get; init; }
}