using Microsoft.Azure.Functions.Worker;
using QuestPDF.Fluent;

namespace DocumentProcessor.Functions;

public class ChecklistPdfGeneratorFunction
{
    private readonly IPdfGenerator _pdfGenerator;
    
    [Function(nameof(ChecklistPdfGeneratorFunction))]
    public async Task Run(
        [ServiceBusTrigger(
            "checklist-submitted", // topic name
            "pdf-generator", // subscription name
            Connection = "ServiceBusConnection")] ChecklistSubmitted message,
        FunctionContext context)

    {
        
        
        
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

    
    private async Task<byte[]> GeneratePdf(Document checklistDocument)
    {
        using var ms = new MemoryStream();
        QuestPDF.Fluent.Document
            .Create(container =>
            {
                container.Page(page =>
                {
                    page.Header().Text($"Checklist: {checklistDocument.Id}");
                    page.Content().Component(new ChecklistComponent(checklistDocument));
                    page.Footer().Text(text =>
                    {
                        text.Span("Generated: ");
                        text.Span(DateTime.Now.ToString("g"));
                    });
                });
            })
            .GeneratePdf(ms);
    
        return ms.ToArray();
    }

}


