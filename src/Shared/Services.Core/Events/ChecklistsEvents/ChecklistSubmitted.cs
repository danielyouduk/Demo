namespace Services.Core.Events.ChecklistsEvents;

public record ChecklistSubmitted
{
    public Guid ChecklistId { get; init; }
    
    public Guid AccountId { get; init; }
    
    public DateTime SubmittedAt { get; init; }
}
