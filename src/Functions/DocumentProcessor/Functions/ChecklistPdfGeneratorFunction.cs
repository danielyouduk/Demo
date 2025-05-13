using DocumentProcessor.Settings;
using MassTransit;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using QuestPDF.Fluent;
using Services.Core.Events.ChecklistsEvents;

namespace DocumentProcessor.Functions;

public class ChecklistPdfGeneratorFunction(
    CosmosClient cosmosClient,
    IConsumer<ChecklistSubmitted> _consumer,
    IOptions<Configuration> configuration)
{
    
    [Function(nameof(ChecklistPdfGeneratorFunction))]
    public async Task Run(
        [ServiceBusTrigger(
            "checklist-submitted",
            "checklist-submitted",
            Connection = "Configuration:AzureServiceBus:ConnectionString")] ChecklistSubmitted message,
        FunctionContext context)
    {
        try
        {
            
            var container = cosmosClient.GetContainer(
                configuration.Value.AzureCosmosDb.DatabaseName,
                configuration.Value.AzureCosmosDb.ContainerName);
            
            var checklist = await container.ReadItemAsync<Checklist>(
                id: message.ChecklistId.ToString(),
                partitionKey: new PartitionKey(message.AccountId.ToString())
            );
            
            var document = new ChecklistDocument(checklist);
            
            document.GeneratePdfAndShow();
            
            
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
        
        
        // foreach (var document in documents)
        // {
        //     // Only process if the document is marked as submitted
        //     if (document.GetPropertyValue<bool>("isSubmitted"))
        //     {
        //         try
        //         {
        //             // Convert checklist to PDF
        //             byte[] pdfContent = await GeneratePdf(document);
        //
        //             // Get blob container
        //             var containerClient = _blobServiceClient.GetBlobContainerClient("checklist-pdfs");
        //             await containerClient.CreateIfNotExistsAsync();
        //
        //             // Generate unique blob name
        //             string blobName = $"checklist-{document.Id}-{DateTime.UtcNow:yyyyMMddHHmmss}.pdf";
        //             
        //             // Upload to blob storage
        //             var blobClient = containerClient.GetBlobClient(blobName);
        //             using (var ms = new MemoryStream(pdfContent))
        //             {
        //                 await blobClient.UploadAsync(ms, overwrite: true);
        //             }
        //
        //             _logger.LogInformation($"PDF generated and stored for checklist {document.Id}");
        //         }
        //         catch (Exception ex)
        //         {
        //             _logger.LogError(ex, $"Error processing checklist {document.Id}");
        //             throw;
        //         }
        //     }
        // }
    }

}


