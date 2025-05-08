using AutoMapper;
using MassTransit;
using MongoDB.Entities;
using SearchService.Api.Models;
using Services.Core.Events.DriverEvents;

namespace SearchService.Api.Consumers;

public class DriverCreatedConsumer : IConsumer<DriverCreated>
{
    public async Task Consume(ConsumeContext<DriverCreated> context)
    {
        try
        {
            var item = new Item
            {
                Identifier = context.Message.DriverId.ToString(),
                Name = $"{context.Message.FirstName} {context.Message.LastName}",
                Type = "Driver",
                AccountId = context.Message.AccountId.ToString(),
                CreatedAt = context.Message.CreatedAt,
                UpdatedAt = context.Message.CreatedAt,
                ResourceUrl = context.Message.ResourceUrl
            };

            await item.SaveAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}