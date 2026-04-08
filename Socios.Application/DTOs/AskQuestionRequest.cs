namespace Socios.Application.DTOs;

public class AskQuestionRequest
{
    public string Query { get; set; } = string.Empty;
    public string? ClubId { get; set; } // Para manejar el multi-tenancy si es necesario
}