// using QuestPDF.Fluent;
//
// namespace DocumentProcessor;
//
// public class PdfGenerator : IPdfGenerator
// {
//     public Task<byte[]> GeneratePdfAsync(Checklist checklist)
//     {
//         // using var ms = new MemoryStream();
//         // QuestPDF.Fluent.Document 
//         //     .Create(container =>
//         //     {
//         //         container.Page(page =>
//         //         {
//         //             page.Header().Text($"Checklist: {checklist.id}");
//         //             page.Content().Component(new ChecklistComponent(checklistDocument));
//         //             page.Footer().Text(text =>
//         //             {
//         //                 text.Span("Generated: ");
//         //                 text.Span(DateTime.Now.ToString("g"));
//         //             });
//         //         });
//         //     })
//         //     .GeneratePdf(ms);
//         //
//         // return ms.ToArray();
//     }
// }