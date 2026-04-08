using Socios.Application.Interfaces;
using UglyToad.PdfPig;
using System.Text;

namespace Socios.Application.Services;

public class DocumentService : IDocumentService
{
    public async Task<bool> ProcessAndStoreDocumentAsync(string fileName, Stream fileStream, string contentType, CancellationToken cancellationToken = default)
    {
        try
        {
            string extractedText = string.Empty;

            // Por ahora solo soportamos PDF para armar el flujo base
            if (contentType == "application/pdf")
            {
                extractedText = ExtractTextFromPdf(fileStream);
            }
            else
            {
                // Si envían otro formato, lo rechazamos por el momento
                return false;
            }

            // Para probar hoy, solo imprimiremos en consola los primeros 500 caracteres
            Console.WriteLine($"\n--- Texto extraído de {fileName} ---");
            Console.WriteLine(extractedText.Substring(0, Math.Min(extractedText.Length, 500)) + "...\n");

            return await Task.FromResult(true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error procesando documento: {ex.Message}");
            return await Task.FromResult(false);
        }
    }

    private string ExtractTextFromPdf(Stream pdfStream)
    {
        var textBuilder = new StringBuilder();

        using (var document = PdfDocument.Open(pdfStream))
        {
            foreach (var page in document.GetPages())
            {
                // Ahora sacamos el texto directamente de la propiedad page.Text
                textBuilder.AppendLine(page.Text);
            }
        }

        return textBuilder.ToString();
    }
}