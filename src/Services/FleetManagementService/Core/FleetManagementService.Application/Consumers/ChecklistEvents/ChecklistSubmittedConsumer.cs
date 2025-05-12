using FleetManagementService.Application.Contracts.Persistence;
using FleetManagementService.Application.Contracts.Persistence.Common;
using MassTransit;
using Services.Core.Events.ChecklistsEvents;

namespace FleetManagementService.Application.Consumers.ChecklistEvents;

public class ChecklistSubmittedConsumer(IAccountRepository accountRepository, IUnitOfWork unitOfWork) : IConsumer<ChecklistSubmitted>
{
    public async Task Consume(ConsumeContext<ChecklistSubmitted> context)
    {
        var checklist = context.Message;
        
        await accountRepository.IncrementChecklistSubmittedCount(checklist);
        await accountRepository.UpdateLastChecklistSubmission(checklist.AccountId, checklist.SubmittedAt);
        
        await unitOfWork.SaveChangesAsync();
    }
}
