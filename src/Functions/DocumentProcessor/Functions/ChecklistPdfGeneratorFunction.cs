using DocumentProcessor.Settings;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Options;
using System.Text.Json;
using Azure.Storage.Blobs;
using DocumentProcessor.Models;
using QuestPDF.Fluent;
using Services.Core.Events.ChecklistsEvents;

namespace DocumentProcessor.Functions;

public class ChecklistPdfGeneratorFunction(
    CosmosClient cosmosClient,
    IOptions<Configuration> configuration,
    BlobServiceClient blobServiceClient)
{
    [Function(nameof(ChecklistPdfGeneratorFunction))]
    public async Task Run(
        [ServiceBusTrigger(
            "checklist-submitted",
            "checklist-submitted-processor",
            Connection = "ServiceBusConnection")] string messageJson,
        FunctionContext context)
    {
        try
        {
            var envelope = JsonSerializer.Deserialize<MassTransitEnvelope<ChecklistSubmitted>>(messageJson);
            var message = envelope.Message;

            var container = cosmosClient.GetContainer(
                configuration.Value.AzureCosmosDb.DatabaseName,
                configuration.Value.AzureCosmosDb.ContainerName);
            
            var checklist = await container.ReadItemAsync<Checklist>(
                id: message.ChecklistId.ToString(),
                partitionKey: new PartitionKey(message.AccountId.ToString())
            );
            
            var document = new ChecklistDocument(checklist);
            var generatedPdf = document.GeneratePdf();

            // Get blob container
            var containerClient = blobServiceClient.GetBlobContainerClient("checklist-pdfs");
            await containerClient.CreateIfNotExistsAsync();
            
            // Generate unique blob name
            string blobName = $"checklist-{message.ChecklistId}-{DateTime.UtcNow:yyyyMMddHHmmss}.pdf";
            
            // Upload to blob storage
            var blobClient = containerClient.GetBlobClient(blobName);
            using (var ms = new MemoryStream(generatedPdf))
            {
                await blobClient.UploadAsync(ms, overwrite: true);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}


