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
            var driverDocument = new Driver(
                id: driverCreated.DriverId.ToString(),
                FirstName: driverCreated.FirstName,
                LastName: driverCreated.LastName,
                CreatedAt: driverCreated.CreatedAt,
                demo: "test"
            );

            // Insert the new driver record into Cosmos DB
            await repository.AddDriverAsync(driverDocument);

            Console.WriteLine($"Driver indexed into Cosmos DB: {driverDocument.FirstName}");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }

}