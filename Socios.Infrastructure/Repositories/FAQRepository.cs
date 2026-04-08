using Microsoft.EntityFrameworkCore;
using Socios.Application.Interfaces;
using Socios.Domain.Entities;
using Socios.Infrastructure.Persistence;

namespace Socios.Infrastructure.Repositories;

public class FAQRepository : IFAQRepository
{
    private readonly SociosDevDbContext _context;

    public FAQRepository(SociosDevDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<FrequentlyQuestion>> SearchRelevantFAQsAsync(string query, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(query))
            return Enumerable.Empty<FrequentlyQuestion>();

        // Lógica de búsqueda simple inicial. 
        // En producción, evaluaremos habilitar Full-Text Search en SQL Server si el volumen crece.
        var lowerQuery = query.ToLower();

        return await _context.FrequentlyQuestions
            // Filtramos por palabras clave o coincidencias en la pregunta
            .Where(f => (f.Question != null && f.Question.ToLower().Contains(lowerQuery)) ||
                        (f.Keywords != null && f.Keywords.ToLower().Contains(lowerQuery)))
            .Take(5) // Límite estricto: prevenimos desbordar la ventana de contexto (tokens) del LLM
            .ToListAsync(cancellationToken);
    }
}