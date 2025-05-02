using MassTransit;
using Services.Core.Events.DriverEvents;

namespace AccountService.Application.Features.Drivers.Consumers;

public class DriverCreatedConsumer : IConsumer<DriverCreated>
{
    public async Task Consume(ConsumeContext<DriverCreated> context)
    {
        var driverCreatedEvent = context.Message;

        // Simulate incrementing the driver count for the AccountId
        Console.WriteLine($"Incrementing driver count for AccountId: {driverCreatedEvent.FirstName}");

        // You can interact with your database or API to perform the operation
        // Example:
        // await _accountService.IncrementDriverCount(driverCreatedEvent.AccountId);

        await Task.CompletedTask;
    }
}