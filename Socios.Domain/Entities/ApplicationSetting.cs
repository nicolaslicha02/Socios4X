namespace Socios.Domain.Entities;

public class ApplicationSetting
{
    // Respetamos la estructura de db_a44a50_clubnaytuel
    // Solo mapeamos los campos que usaremos para el asistente (Name, Logo, etc.)
    public int Id { get; private set; }
    public string? Name { get; private set; }
    public string? Description { get; private set; }
    public string? Address { get; private set; }
    public string? PhoneNumber { get; private set; }
    public string? WhatsApp { get; private set; }
    // El logo viene como varbinary(max), en C# es byte[]
    public byte[]? Logo { get; private set; }

    protected ApplicationSetting() { }
}