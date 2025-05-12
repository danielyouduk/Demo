using FleetManagementService.Application.Contracts.Persistence;
using MassTransit;
using Services.Core.Events.ChecklistsEvents;

namespace FleetManagementService.Application.Consumers.ChecklistEvents;

public class ChecklistAccountUpdatesConsumer(IAccountRepository accountRepository) : IConsumer<ChecklistSubmitted>
{
    public async Task Consume(ConsumeContext<ChecklistSubmitted> context)
    {
        var checklist = context.Message;
        
        await accountRepository.IncrementChecklistCount(checklist);
        await accountRepository.UpdateLastChecklistSubmission(checklist.AccountId, checklist.SubmittedAt);
    }
}
