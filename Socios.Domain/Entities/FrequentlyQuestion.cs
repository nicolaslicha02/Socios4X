namespace Socios.Domain.Entities;

public class FrequentlyQuestion
{
    // Respetamos la estructura original de db_a44a50_sociosdev
    public int Id { get; private set; }
    public string? Question { get; private set; }
    public string? Answer { get; private set; }
    public string? Category { get; private set; }
    public string? Keywords { get; private set; }
    public string? UsersRole { get; private set; }
    public int? Frequently { get; private set; }
    public DateTime? UpdateDate { get; private set; }

    protected FrequentlyQuestion() { } // Para EF Core
}