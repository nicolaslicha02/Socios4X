using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Socios.Infrastructure.Persistence;
using Socios.Application.Interfaces;

namespace Socios.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // 1. Persistencia Tradicional (SQL Server)
        services.AddDbContext<SociosDevDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("SociosDevConnection")));

        services.AddDbContext<ClubDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultClubConnection")));

        // 2. Inteligencia Artificial (Semantic Kernel)
        var aiConfig = configuration.GetSection("AI");

        // Usamos null-coalescing para evitar caídas si el string está vacío en desarrollo
        var openAiKey = aiConfig["OpenAI:ApiKey"] ?? "TEST_KEY";
        var chatModel = aiConfig["OpenAI:ChatModel"] ?? "gpt-4o-mini";
        var embeddingModel = aiConfig["OpenAI:EmbeddingModel"] ?? "text-embedding-3-small";

        // Registramos el Kernel y los servicios de OpenAI
        services.AddKernel()
            .AddOpenAIChatCompletion(chatModel, openAiKey)
            .AddOpenAITextEmbeddingGeneration(embeddingModel, openAiKey);

        // La configuración específica del cliente de Qdrant la inyectaremos 
        // cuando armemos el repositorio vectorial.

        // 3. Repositorios
        services.AddScoped<IFAQRepository, Socios.Infrastructure.Repositories.FAQRepository>();

        return services;
    }
}