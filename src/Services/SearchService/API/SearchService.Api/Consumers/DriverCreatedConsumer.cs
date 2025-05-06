using MassTransit;
using SearchService.Api.Models;
using SearchService.Api.Repositories;
using Services.Core.Events.DriverEvents;

namespace SearchService.Api.Consumers;

public class DriverCreatedConsumer(IDriverCosmosRepository repository) : IConsumer<DriverCreated>
{
    public async Task Consume(ConsumeContext<DriverCreated> context)
    {
        try
        {
            var driverCreated = context.Message;
            var entity = new DriverEntity
            {
                PartitionKey = "Driver",
                RowKey = Guid.NewGuid().ToString(),
                Category = "Driver",
                Title = driverCreated.FirstName + " " + driverCreated.LastName,
                Url = "/drivers/12345"
            };
            
            await repository.AddDriverAsync(entity);

            Console.WriteLine($"Driver indexed into Azure Table Storage: {entity.Timestamp}");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}