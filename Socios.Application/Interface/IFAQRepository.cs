using Socios.Domain.Entities;

namespace Socios.Application.Interfaces;

public interface IFAQRepository
{
    // Busca FAQs que coincidan semánticamente o por palabras clave
    Task<IEnumerable<FrequentlyQuestion>> SearchRelevantFAQsAsync(string query, CancellationToken cancellationToken = default);
}