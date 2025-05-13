using MassTransit;
using Services.Core.Events.ChecklistsEvents;

namespace DocumentProcessor;

public class ChecklistSubmittedConsumer : IConsumer<ChecklistSubmitted>
{
    public async Task Consume(ConsumeContext<ChecklistSubmitted> context)
    {
        throw new NotImplementedException();
    }
}