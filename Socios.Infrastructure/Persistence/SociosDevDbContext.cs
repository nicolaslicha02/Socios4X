using Microsoft.EntityFrameworkCore;
using Socios.Domain.Entities;

namespace Socios.Infrastructure.Persistence;

public class SociosDevDbContext : DbContext
{
    public SociosDevDbContext(DbContextOptions<SociosDevDbContext> options) : base(options) { }

    public DbSet<FrequentlyQuestion> FrequentlyQuestions { get; set; }
    public DbSet<Document> Documents { get; set; } // La que creamos en el Módulo 2

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FrequentlyQuestion>(entity =>
        {
            entity.ToTable("FrequentlyQuestions");
            entity.HasKey(e => e.Id);
        });

        // Configuración de nuestra nueva tabla Documents
        modelBuilder.Entity<Document>(entity =>
        {
            entity.ToTable("Documents");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FileName).IsRequired();
            entity.Property(e => e.FileType).IsRequired();
        });
    }
}