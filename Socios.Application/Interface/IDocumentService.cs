namespace Socios.Application.Interfaces;

public interface IDocumentService
{
    // Recibe el archivo físico, lo procesa y guarda los embeddings
    Task<bool> ProcessAndStoreDocumentAsync(string fileName, Stream fileStream, string contentType, CancellationToken cancellationToken = default);
}