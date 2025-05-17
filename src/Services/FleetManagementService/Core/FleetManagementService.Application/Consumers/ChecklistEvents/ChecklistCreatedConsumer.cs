using FleetManagementService.Application.Contracts.Persistence;
using FleetManagementService.Application.Contracts.Persistence.Common;
using MassTransit;
using Services.Core.Events.ChecklistsEvents;

namespace FleetManagementService.Application.Consumers.ChecklistEvents;

public class ChecklistCreatedConsumer(IAccountRepository accountRepository, IUnitOfWork unitOfWork) : IConsumer<ChecklistCreated>
{
    public async Task Consume(ConsumeContext<ChecklistCreated> context)
    {
        var checklist = context.Message;

        await accountRepository.UpdateChecklistCreated(checklist, context.CancellationToken);

        await unitOfWork.SaveChangesAsync();
    }
}