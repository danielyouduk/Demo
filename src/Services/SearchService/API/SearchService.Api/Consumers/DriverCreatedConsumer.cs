using MassTransit;
using SearchService.Api.Repositories;
using Services.Core.Events.DriverEvents;

namespace SearchService.Api.Consumers;

public class DriverCreatedConsumer(DriverCosmosRepository repository) : IConsumer<DriverCreated>
{
    public async Task Consume(ConsumeContext<DriverCreated> context)
    {
        try
        {
            var driverCreated = context.Message;

            // Check if the document already exists in Cosmos DB
            var existingDriver = await repository.GetDriverByIdAsync(driverCreated.DriverId.ToString());
            if (existingDriver != null)
            {
                Console.WriteLine($"Driver with ID {driverCreated.DriverId} already indexed. Skipping.");
                return; // Prevent duplicate insertion
            }

            // Map to the document model
            var driverDocument = new DriverCosmosRepository.DriverDocument
            {
                Id = driverCreated.DriverId.ToString(),
                FirstName = driverCreated.FirstName,
                LastName = driverCreated.LastName,
                CreatedAt = driverCreated.CreatedAt
            };

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